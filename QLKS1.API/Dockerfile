# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy toàn bộ source code vào container
COPY . ./

# Publish ứng dụng về thư mục /out
RUN dotnet publish  QLKS1.API.csproj -c Release -o out

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy từ stage build vào stage runtime
COPY --from=build /app/out ./

# Cấu hình lệnh khởi động container
ENTRYPOINT ["dotnet", "QLKS1.API.dll"]
