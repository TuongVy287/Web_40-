from fastapi import FastAPI, UploadFile, File
from fastapi.middleware.cors import CORSMiddleware
from paddleocr import PaddleOCR
import numpy as np
import cv2
import io
from PIL import Image
import re
from typing import List, Dict, Any

app = FastAPI()
ocr = PaddleOCR(use_angle_cls=True, lang='vi')

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # ["http://localhost:5173"] nếu dùng Vite
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

def extract_info_from_ocr_result(ocr_result: List[Any]) -> Dict[str, str]:
    text_lines = [line[1][0] for line in ocr_result if len(line) > 1 and line[1]]
    text = "\n".join(text_lines)

    def find_by_keywords(keywords, lines=text_lines):
        for i, line in enumerate(lines):
            for key in keywords:
                if key.lower() in line.lower():
                    # Trường hợp cùng dòng, có dấu ":"
                    parts = line.split(":")
                    if len(parts) > 1:
                        return parts[1].strip()
                    # Nếu không có ":" mà là dòng riêng -> lấy dòng sau
                    elif i + 1 < len(lines):
                        return lines[i + 1].strip()
        return ""


    def match_regex(pattern):
        match = re.search(pattern, text)
        return match.group(0) if match else ""

    return {
        "soCCCD": match_regex(r"\d{12}"),
        "hoTen": find_by_keywords(["Họ và tên", "Họ tên", "Full name"]),
        "ngaySinh": match_regex(r"\d{2}/\d{2}/\d{4}"),
        "gioiTinh": find_by_keywords(["Giới tính", "Sex"]),
        "quocTich": find_by_keywords(["Quốc tịch", "Nationality"]),
        "queQuan": find_by_keywords(["Quê quán", "Place of origin"]),
        "noiThuongTru": find_by_keywords(["Nơi thường trú", "Place of residence"]),
        "coGiaTriDen": match_regex(r"Có giá trị đến[:\s]*([0-9]{2}/[0-9]{2}/[0-9]{4})")
                         or find_by_keywords(["Có giá trị đến", "Date of expiry"]),
        "full_text": text  # thêm để debug
    }

@app.post("/ocr")
async def ocr_image(file: UploadFile = File(...)):
    img = Image.open(io.BytesIO(await file.read())).convert("RGB")
    img = np.array(img)

    result = ocr.ocr(img, cls=True)
    ocr_lines = result[0]

    info = extract_info_from_ocr_result(ocr_lines)

    return info
