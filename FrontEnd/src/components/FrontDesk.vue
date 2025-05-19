<template>
  <div class="front-desk-container">
    <div class="front-desk-header">
      <h2>Front desk</h2>
    </div>

    <div class="search-and-create">
      <div class="status-buttons">
        <span class="status-badge duein">Due in</span>
        <span class="status-badge checkedout">Checked out</span>
        <span class="status-badge dueout">Due out</span>
        <span class="status-badge checkedin">Checked in</span>
      </div>
      <input type="text" placeholder="Search by room number" class="room-search" />
      <router-link to="/create-booking" class="create-booking-button">
        Create booking
      </router-link>
    </div>

    <div class="months-row">
      <div
        v-for="(month, index) in months"
        :key="`month-${index}`"
        :class="['month-item', { active: index === selectedMonth }]"
        @click="selectMonth(index)"
      >
        {{ month }}
      </div>
    </div>

    <div class="timeline">
      <div class="scroll-wrapper">
        <!-- Ngày -->
        <div class="days-row">
          <div
            v-for="day in days"
            :key="`day-${day}`"
            class="day-box"
            :class="{ today: isToday(day) && !isSelectedToday, selected: selectedDay === day }"
            :ref="isToday(day) ? 'todayRef' : null"
            @click="selectedDay = day"
          >
            {{ day }}
          </div>
        </div>

        <!-- Booking theo ngày -->
        <div class="bookings-row">
          <div class="booking-day-slot" style="position: relative; min-height: 100px;">
            <div
              v-for="(booking, index) in bookings"
              :key="`booking-${index}`"
              class="booking-block"
              :style="{
                backgroundColor: booking.color,
                left: (booking.startDay - 1) * dayWidth + 'px',
                width: (booking.endDay - booking.startDay + 1) * dayWidth - dayGap + 'px',
                top: booking.top + 'px'
              }"
              :title="`Phòng: ${booking.tenPhong.trim()}, Trạng thái: ${booking.trangThaiDatPhong}`"
            >
              {{ booking.tenPhong.trim() }} - {{ booking.trangThaiDatPhong }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "FrontDesk",
  data() {
    return {
      selectedMonth: new Date().getMonth(),
      selectedDay: null,
      months: [
        "Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
      ],
      bookings: [],
      dayWidth: 100,
      dayGap: 10,
    };
  },
  computed: {
    days() {
      const year = new Date().getFullYear();
      const realMonth = this.selectedMonth;
      const daysInMonth = new Date(year, realMonth + 1, 0).getDate();
      return Array.from({ length: daysInMonth }, (_, i) => i + 1);
    },
    isSelectedToday() {
      const today = new Date();
      return this.selectedDay === today.getDate() && this.selectedMonth === today.getMonth();
    },
  },
  watch: {
    selectedMonth() {
      this.loadBookings();
    },
  },
  mounted() {
    const today = new Date();
    if (this.selectedMonth === today.getMonth()) {
      this.selectedDay = today.getDate();
    }
    this.loadBookings();
  },
  methods: {
    isToday(day) {
      const today = new Date();
      return day === today.getDate() && this.selectedMonth === today.getMonth();
    },
    selectMonth(index) {
      this.selectedMonth = index;
      const today = new Date();
      this.selectedDay = index === today.getMonth() ? today.getDate() : null;
    },
    async loadBookings() {
      try {
        const tenThang = this.months[this.selectedMonth].toLowerCase();
        const response = await fetch(
          `http://localhost:5250/api/DatPhong/theo-thang?thangChu=${tenThang}`
        );
        const data = await response.json();

        const year = new Date().getFullYear();
        const month = this.selectedMonth;

        const bookingsInMonth = data
          .map((booking) => {
            const ngayNhan = new Date(booking.ngayNhan);
            const ngayTra = new Date(booking.ngayTra);

            if (
              ngayNhan.getFullYear() === year &&
              ngayNhan.getMonth() === month &&
              ngayTra.getFullYear() === year &&
              ngayTra.getMonth() === month
            ) {
              return {
                ...booking,
                startDay: ngayNhan.getDate(),
                endDay: ngayTra.getDate(),
                color: this.getColorByStatus(booking.trangThaiDatPhong),
              };
            }
            return null;
          })
          .filter(Boolean)
          .sort((a, b) => a.startDay - b.startDay);

        const rows = [];

        bookingsInMonth.forEach((booking) => {
          let placed = false;
          for (let i = 0; i < rows.length; i++) {
            const row = rows[i];
            const overlap = row.some(
              (b) => !(booking.endDay < b.startDay || booking.startDay > b.endDay)
            );
            if (!overlap) {
              booking.top = i * 30;
              row.push(booking);
              placed = true;
              break;
            }
          }
          if (!placed) {
            booking.top = rows.length * 30;
            rows.push([booking]);
          }
        });

        this.bookings = bookingsInMonth;
      } catch (error) {
        console.error("Lỗi khi load bookings:", error);
      }
    },
    getColorByStatus(status) {
      switch (status) {
        case "Xác nhận":
          return "#f39c12";
        case "Đã nhận phòng":
          return "#27ae60";
        case "Đã trả phòng":
          return "#3498db";
        default:
          return "#888";
      }
    },
  },
};
</script>
<style scoped>
.front-desk-container {
  margin: 0 auto;
  font-family: sans-serif;
  background: #fafafa;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
  margin-left: 50px;
  margin-top: 80px;
}

.front-desk-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 20px;
}

.front-desk-header h2 {
  font-size: 24px;
  color: #f39c12;
  margin: 0;
}

.status-buttons {
  display: flex;
  gap: 10px;
}

.status-badge {
  padding: 5px 10px;
  border-radius: 20px;
  font-size: 14px;
  color: #fff;
}

.duein {
  background-color: #ffe59e;
  color: #333;
}

.checkedout {
  background-color: #add8ff;
  color: #333;
}

.dueout {
  background-color: #ffd7d7;
  color: #333;
}

.checkedin {
  background-color: #b9f7ca;
  color: #333;
}

.search-and-create {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.room-search {
  width: 300px;
  padding: 8px 12px;
  border-radius: 20px;
  border: 1px solid #ccc;
  outline: none;
}

.create-booking-button {
  background-color: #f39c12;
  color: #fff;
  padding: 10px 20px;
  border-radius: 20px;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.create-booking-button:hover {
  background-color: #e08f08;
}

.months-row {
  display: flex;
  gap: 15px;
  margin-bottom: 20px;
}

.month-item {
  padding: 8px 16px;
  background-color: #f1f1f1;
  border-radius: 20px;
  cursor: pointer;
  color: #333;
  transition: background-color 0.3s ease;
}

.month-item:hover {
  background-color: #e9e9e9;
}

.month-item.active {
  background-color: #f39c12;
  color: #fff;
}

.timeline {
  border-top: 1px solid #ddd;
  padding-top: 10px;
  overflow-x: auto;
  position: relative;
}

.scroll-wrapper {
  overflow-x: auto;
  position: relative;
  min-width: 100%;
}

/* Hàng hiển thị ngày */
.days-row {
  display: flex;
  flex-wrap: nowrap;
  min-width: max-content;
}

.day-box {
  width: 100px; /* Trùng với dayWidth trong js */
  height: 40px;
  text-align: center;
  font-weight: bold;
  color: #000;
  
  line-height: 40px;
  border-radius: 8px;
  transition: all 0.3s ease;
  cursor: pointer;
}

.day-box.today {
  transform: scale(1.05);
  opacity: 0.6;
  border: 2px solid #f39c12;
  border-radius: 50%;
  color: #f39c12;
  font-weight: bold;
}

.day-box.selected {
  transform: scale(1.1);
  opacity: 1 !important;
  border: 2px solid #f39c12;
  border-radius: 50%;
  color: #1b1a19;
  font-weight: bold;
  background-color: transparent;
  box-shadow: 0 0 8px rgba(243, 156, 18, 0.6);
}

.day-box:hover {
  background-color: #f39c12;
}

/* Khung booking, vị trí relative để chứa các block booking position absolute */
.bookings-row {
  position: relative;
  min-height: 200px;
  margin-top: 10px;
}

/* Các block booking hiện tại */
.booking-block {
  position: absolute;
  height: 25px;
  line-height: 25px;
  color: #fff;
  font-size: 13px;
  border-radius: 4px;
  padding: 0 10px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  cursor: pointer;
  box-shadow: 0 0 5px rgba(0,0,0,0.2);
  user-select: none;
  
}
</style>
