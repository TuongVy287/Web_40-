<template>
  <div class="p-4 nhanvien">
    <h2 class="text-xl font-bold mb-2">Nhận dạng CCCD</h2>
    <input type="file" @change="onFileChange" accept="image/*" />
    <div v-if="loading">⏳ Đang xử lý ảnh...</div>
    <pre v-if="result" class="bg-gray-100 p-2 mt-4">{{ result }}</pre>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const result = ref(null)
const loading = ref(false)

const onFileChange = async (e) => {
  const file = e.target.files[0]
  if (!file) return

  const formData = new FormData()
  formData.append('file', file)

  loading.value = true
  try {
    const res = await axios.post('${API_BASE_URL}ocr', formData)
    result.value = res.data
    console.log('Kết quả nhận dạng:', res.data)
  } catch (err) {
    result.value = 'Lỗi khi gửi ảnh: ' + err
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.nhanvien {
  margin-left: 50px;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 1%;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
  margin-top: 80px;
}
</style>
