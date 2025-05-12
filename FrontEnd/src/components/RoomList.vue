<template>
  <div class="room-list">
    <h2>Room</h2>

    <div class="room-status-buttons">
      <button @click="fetchRooms('Tất cả')">
        All room ({{ rooms.length }})
      </button>
      <button @click="fetchRooms('Trống')">
        Available room ({{ availableCount }})
      </button>
      <button @click="fetchRooms('Đã đặt')">
        Booked ({{ bookedCount }})
      </button>
    </div>

    <table>
      <thead>
        <tr>
          <th>Room ID</th>
          <th>Room Type</th>
          <th>Capacity</th>
          <th>Price</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="room in paginatedRooms" :key="room.idPhong">
          <td>{{ room.idPhong }}</td>
          <td>{{ room.loaiPhong }}</td>
          <td>{{ room.suChua }}</td>
          <td>{{ room.gia.toLocaleString() }} VND</td>
          <td :class="room.trangThaiPhong.toLowerCase().replace(/\s/g, '-')">
            {{ room.trangThaiPhong }}
          </td>
        </tr>
      </tbody>
    </table>

    <div class="pagination">
      <button @click="prevPage" :disabled="currentPage === 1">Previous</button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button @click="nextPage" :disabled="currentPage === totalPages">Next</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "RoomList",
  data() {
    return {
      rooms: [],
      currentStatus: "Tất cả",
      currentPage: 1,
      pageSize: 4,
    };
  },
  computed: {
    availableCount() {
      return this.rooms.filter((r) => r.trangThaiPhong === "Trống").length;
    },
    bookedCount() {
      return this.rooms.filter((r) => r.trangThaiPhong === "Đã đặt").length;
    },
    paginatedRooms() {
      const start = (this.currentPage - 1) * this.pageSize;
      return this.rooms.slice(start, start + this.pageSize);
    },
    totalPages() {
      return Math.ceil(this.rooms.length / this.pageSize);
    },
  },
  created() {
    this.fetchRooms(this.currentStatus);
  },
  methods: {
    async fetchRooms(status) {
      try {
        this.currentStatus = status;
        this.currentPage = 1;
        const response = await axios.get(
          `http://localhost:5250/api/Phong/theo-trang-thai?trangThai=${status}`
        );
        this.rooms = response.data;
      } catch (error) {
        console.error("Error fetching rooms:", error);
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
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
  min-height: 265px;
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

/* Đổi màu trạng thái */
.trống {
  color: green;
}

.đã-đặt {
  color: red;
}

.bảo-trì {
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
