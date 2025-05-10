<template>
  <div class="room-list">
    <h2>Room</h2>
    <div class="room-status-buttons">
      <!-- Tuỳ ý bạn xử lý logic hiển thị tổng số, phòng trống, phòng đã đặt -->
      <button>All room({{ rooms.length }})</button>
      <button>Available room( ... )</button>
      <button>Booked( ... )</button>
    </div>
    <table>
      <thead>
        <tr>
          <th>Room number</th>
          <th>Bed type</th>
          <th>Room floor</th>
          <th>Room facility</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="room in rooms" :key="room.number">
          <td>{{ room.number }}</td>
          <td>{{ room.bedType }}</td>
          <td>{{ room.floor }}</td>
          <td>{{ room.facility }}</td>
          <!-- Gắn class theo trạng thái để đổi màu chữ -->
          <td :class="room.status.toLowerCase()">{{ room.status }}</td>
        </tr>
      </tbody>
    </table>
    <div class="pagination">
      <button>Previous</button>
      <span>1</span>
      <button>Next</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "RoomList",
  data() {
    return {
      rooms: [], // Chứa danh sách phòng lấy từ API
    };
  },
  created() {
    this.fetchRooms();
  },
  methods: {
    async fetchRooms() {
      try {
        // Gọi API để lấy dữ liệu
        const response = await axios.get(`http://localhost:5250/api/ThongKe/homnay`);
        // Gán dữ liệu trả về cho rooms
        this.rooms = response.data;
        console.log("Rooms fetched successfully:", this.rooms);
      } catch (error) {
        console.error("Error fetching rooms:", error);
      }
    },
  },
};
</script>

<style scoped>
.room-list {
  margin-left: 50px;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 1%;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
  margin-top: 80px;
}

h2 {
  color: #f39c12;
  margin-bottom: 20px;
  font-size: 24px;
}

.room-status-buttons {
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
}

.room-status-buttons button {
  padding: 12px 20px;
  background-color: #f39c12;
  color: white;
  border: none;
  border-radius: 30px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.room-status-buttons button:hover {
  background-color: #e67e22;
  transform: scale(1.05);
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

th,
td {
  padding: 14px;
  border: 1px solid #ddd;
  text-align: center;
  font-size: 16px;
  border-radius: 8px;
}

th {
  background-color: #f1f1f1;
  color: #333;
}

td {
  background-color: #fafafa;
}

td:hover {
  background-color: #f1f1f1;
}

tr:nth-child(even) {
  background-color: #f9f9f9;
}

tr:hover {
  background-color: #e9e9e9;
}

/* Ví dụ các class đổi màu chữ tuỳ theo trạng thái */
.available {
  color: green;
}
.booked {
  color: red;
}
.reserved {
  color: orange;
}
.waitlist {
  color: yellow;
}
.blocked {
  color: gray;
}

.pagination {
  margin-top: 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pagination button {
  padding: 12px 20px;
  background-color: #f39c12;
  color: white;
  border: none;
  border-radius: 30px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.pagination button:hover {
  background-color: #e67e22;
  transform: scale(1.05);
}
</style>
