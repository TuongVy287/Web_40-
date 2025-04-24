<template>
  <div class="rate">
    <h2>Rate</h2>
    <div class="rate-controls">
      <button class="add-rate-button" @click="showModal">Add rate</button>
      <button class="filter-button">Filter</button>
    </div>
    <RateModal v-if="isModalVisible" @close="closeModal" />
    <table>
      <thead>
        <tr>
          <th>Room type</th>
          <th>Deals</th>
          <th>Cancellation policy</th>
          <th>Deal price</th>
          <th>Rate</th>
          <th>Availability</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="room in rates" :key="room.type">
          <td>{{ room.roomType }}</td>
          <td>{{ room.deals }}</td>
          <td>{{ room.cancellationPolicy }}</td>
          <td>{{ room.dealPrice }}</td>
          <td>{{ room.rate }}</td>
          <td>{{ room.availability }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import axios from 'axios'; // Import Axios
import RateModal from './RateModal.vue';

export default {
  name: "Rate",
  components: {
    RateModal,
  },
  data() {
    return {
      isModalVisible: false,
      rates: [], // Khởi tạo empty array để chứa dữ liệu
    };
  },
  created() {
    this.fetchRates(); // Lấy dữ liệu API khi component được tạo
  },
  methods: {
    async fetchRates() {
      try {
        const response = await axios.get('http://localhost:3000/api/rates'); // Đóng URL API
        this.rates = response.data; // Gán dữ liệu vào rates
      } catch (error) {
        console.error('Error fetching rates:', error);
      }
    },
    showModal() {
      this.isModalVisible = true; // Hiển thị modal
    },
    closeModal() {
      this.isModalVisible = false; // Đóng modal
    }
  }
};
</script>


<style scoped>
.rate {
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

.rate-controls {
    display: flex;
    justify-content: space-between;
    margin-bottom: 20px;
}

.add-rate-button,
.filter-button {
    padding: 12px 20px;
    background-color: #f39c12;
    color: white;
    border: none;
    border-radius: 30px;
    cursor: pointer;
    font-size: 16px;
    transition: background-color 0.3s ease, transform 0.3s ease;
}

.add-rate-button:hover,
.filter-button:hover {
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
</style>
