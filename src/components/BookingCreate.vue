<template>
    <div class="booking-create-page">
      <div class="CheckCheck">
        <!-- Room Type Filter Buttons -->
        <div class="room-type-buttons">
          <button :class="['room-btn', selectedRoomType === 'all' ? 'active' : '']" @click="selectedRoomType = 'all'">
            All room ({{ rooms.length }})
          </button>
          <button :class="['room-btn', selectedRoomType === 'available' ? 'active' : '']" @click="selectedRoomType = 'available'">
            Available room ({{ availableRooms.length }})
          </button>
          <button :class="['room-btn', selectedRoomType === 'booked' ? 'active' : '']" @click="selectedRoomType = 'booked'">
            Booked ({{ bookedRooms.length }})
          </button>
        </div>
  
        <!-- Date Pickers -->
        <div class="date-row">
          <div>
            <label>Check in</label>
            <input type="date" v-model="form.checkIn" />
          </div>
          <div>
            <label>Check out</label>
            <input type="date" v-model="form.checkOut" />
          </div>
        </div>
  
        <!-- Guest Counter -->
        <div class="guest-counter-row">
          <div class="guest-type">
            <label>Adult<br><small>Older than 12 years</small></label>
            <div class="counter">
              <button @click="form.adults = Math.max(1, form.adults - 1)">-</button>
              <span>{{ form.adults }}</span>
              <button @click="form.adults++">+</button>
            </div>
          </div>
          <div class="guest-type">
            <label>Children<br><small>0 - 12 years</small></label>
            <div class="counter">
              <button @click="form.children = Math.max(0, form.children - 1)">-</button>
              <span>{{ form.children }}</span>
              <button @click="form.children++">+</button>
            </div>
          </div>
        </div>
  
        <!-- Check availability -->
        <div class="check-btn-wrapper">
          <button class="check-button" @click="submitBooking">
            Check availability
          </button>
        </div>
      </div>
  
      <!-- Booked Room List -->
      <div class='table-booker-room'>
        <h3>Booked Rooms</h3>
        <table class="room-table">
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
            <tr v-for="room in filteredRooms" :key="room.roomNumber">
              <td>{{ room.roomNumber }}</td>
              <td>{{ room.bedType }}</td>
              <td>{{ room.floor }}</td>
              <td>{{ room.facility }}</td>
              <td>
                <span :class="['status-label', room.status.toLowerCase()]">{{ room.status }}</span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'BookingCreate',
    data() {
      return {
        form: {
          checkIn: '',
          checkOut: '',
          adults: 1,
          children: 0,
        },
        selectedRoomType: 'all',
        rooms: [],
      };
    },
    computed: {
      availableRooms() {
        return this.rooms.filter(room => room.status.toLowerCase() === 'available');
      },
      bookedRooms() {
        return this.rooms.filter(room => room.status.toLowerCase() === 'booked');
      },
      filteredRooms() {
        if (this.selectedRoomType === 'available') {
          return this.availableRooms;
        } else if (this.selectedRoomType === 'booked') {
          return this.bookedRooms;
        } else {
          return this.rooms;
        }
      }
    },
    methods: {
      async fetchRooms() {
        try {
          const res = await fetch('https://your-api.com/booked-rooms');
          this.rooms = await res.json();
        } catch (e) {
          console.error('Failed to fetch rooms', e);
        }
      },
      async submitBooking() {
        try {
          const res = await fetch('https://your-api.com/bookings', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.form),
          });
          if (res.ok) {
            alert('Booking created successfully');
            this.$router.push('/front-desk');
          } else {
            alert('Failed to create booking');
          }
        } catch (e) {
          console.error(e);
        }
      },
    },
    mounted() {
      this.fetchRooms();
    },
  };
  </script>
  
  <style scoped>
  .CheckCheck,
  .table-booker-room {
    margin: 0 auto;
    font-family: sans-serif;
    background: #fafafa;
    border-radius: 8px;
    padding: 20px;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
    margin-left: 100px;
    margin-top: 80px;
  }
  
  .room-type-buttons {
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
    margin-bottom: 24px;
  }
  .room-btn {
    padding: 8px 14px;
    border-radius: 20px;
    border: 1px solid #ffc107;
    background: #fffbe6;
    color: #ea9c00;
    font-weight: 600;
    cursor: pointer;
    transition: all 0.2s ease;
  }
  .room-btn.active {
    background: #ea9c00;
    color: #fff;
    border-color: transparent;
  }
  
  .date-row {
    display: flex;
    gap: 32px;
    margin-bottom: 20px;
  }
  .date-row label {
    font-weight: 600;
    margin-bottom: 4px;
    display: block;
  }
  input[type="date"] {
    padding: 8px;
    border-radius: 8px;
    border: 1px solid #ccc;
    font-size: 14px;
  }
  
  .guest-counter-row {
    display: flex;
    gap: 40px;
    margin-bottom: 24px;
  }
  .guest-type label {
    font-weight: 600;
    font-size: 14px;
  }
  .guest-type small {
    display: block;
    font-size: 12px;
    color: #999;
  }
  .counter {
    display: flex;
    align-items: center;
    margin-top: 6px;
  }
  .counter button {
    width: 30px;
    height: 30px;
    font-size: 16px;
    background: #ffe8c7;
    border: none;
    color: #ea9c00;
    cursor: pointer;
    border-radius: 50%;
    font-weight: bold;
  }
  .counter button:hover {
    background-color: #fcd59c;
  }
  .counter span {
    width: 30px;
    text-align: center;
    font-weight: 600;
  }
  
  .check-btn-wrapper {
    text-align: right;
    margin-bottom: 32px;
  }
  .check-button {
    background: #ea9c00;
    color: white;
    border: none;
    padding: 10px 18px;
    border-radius: 10px;
    font-weight: 600;
    font-size: 15px;
    cursor: pointer;
    transition: 0.2s ease;
  }
  .check-button:hover {
    background: #d48806;
  }
  
  h3 {
    color: #ea9c00;
    margin-bottom: 12px;
    font-size: 20px;
    font-weight: 700;
  }
  .room-table {
    width: 100%;
    border-collapse: collapse;
    background: #fff;
    border-radius: 12px;
    overflow: hidden;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
  }
  .room-table th, .room-table td {
    padding: 14px 12px;
    text-align: left;
    font-size: 14px;
  }
  .room-table th {
    background: #fafafa;
    font-weight: 600;
    color: #444;
  }
  .room-table tbody tr:hover {
    background-color: #fdf5e6;
  }
  .room-table td {
    border-bottom: 1px solid #f0f0f0;
  }
  .status-label {
    padding: 6px 10px;
    border-radius: 10px;
    font-size: 12px;
    font-weight: 600;
    text-transform: capitalize;
    color: white;
  }
  .status-label.available {
    background-color: #28a745;
  }
  .status-label.booked {
    background-color: #dc3545;
  }
  .status-label.reserved {
    background-color: #17a2b8;
  }
  </style>
  