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
            <div v-for="(month, index) in months" :key="index"
                :class="['month-item', { active: index === selectedMonth }]" @click="selectMonth(index)">
                {{ month }}
            </div>
        </div>

        <!-- Timeline -->
        <div class="timeline">
            <div class="scroll-wrapper">
                <!-- Hàng hiển thị ngày -->
                <div class="days-row">
                    <div v-for="day in days" :key="day" class="day-box" :class="{
                        today: isToday(day) && !isSelectedToday,
                        selected: selectedDay === day
                    }" @click="selectedDay = day">
                        {{ day }}
                    </div>
                </div>

                <!-- Hàng booking -->
                <div class="bookings-row">
                    <div v-for="(dayBookings, dayIndex) in bookings" :key="dayIndex" class="booking-day-slot">
                        <div v-for="(booking, bIndex) in dayBookings" :key="bIndex" class="booking-block"
                            :style="{ backgroundColor: booking.color }"
                            :title="`Guest: ${booking.name}, Room: ${booking.roomNumber}`">
                            {{ booking.name }}
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
            showModal: false,
            selectedMonth: (new Date().getMonth() + 12 - 3) % 12, // 'Apr' là index 0 => shift về 3 tháng
            selectedDay: null,
            months: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            bookings: [],
            newBooking: {
                name: "",
                roomNumber: "",
                day: 1,
                color: "#f39c12",
            },
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
        }
    },
    watch: {
        days(newDays) {
            const currentLength = this.bookings.length;
            const targetLength = newDays.length;
            if (currentLength < targetLength) {
                for (let i = currentLength; i < targetLength; i++) {
                    this.bookings.push([]);
                }
            } else if (currentLength > targetLength) {
                this.bookings.splice(targetLength);
            }
        },
        selectedMonth() {
            this.loadBookings();
        }
    },
    mounted() {
        this.bookings = Array.from({ length: this.days.length }, () => []);
        const today = new Date();
        if (this.selectedMonth === today.getMonth()) {
            this.selectedDay = today.getDate();
        }
        this.loadBookings();
    },
    methods: {
        goToBookingPage() {
            this.$router.push('/create-booking');
        },
        isToday(day) {
            const today = new Date();
            return day === today.getDate() && this.selectedMonth === today.getMonth();
        },
        selectMonth(index) {
            this.selectedMonth = index;
            const today = new Date();
            if (index === today.getMonth()) {
                this.selectedDay = today.getDate();
            } else {
                this.selectedDay = null;
            }
        },
        async loadBookings() {
            try {
                const response = await fetch(`https://your-api.com/bookings?month=${this.selectedMonth + 1}`);
                const data = await response.json();
                const tempBookings = Array.from({ length: this.days.length }, () => []);
                data.forEach(booking => {
                    if (booking.day >= 1 && booking.day <= this.days.length) {
                        tempBookings[booking.day - 1].push({
                            name: booking.name,
                            roomNumber: booking.roomNumber,
                            color: booking.color || '#f39c12',
                        });
                    }
                });
                this.bookings = tempBookings;
            } catch (error) {
                console.error("Failed to load bookings:", error);
            }
        },
    },
};
</script>



<style scoped>
.front-desk-container {
    max-width: 1200px;
    margin: 0 auto;
    font-family: sans-serif;
    background: #fafafa;
    border-radius: 8px;
    padding: 20px;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
    margin-left: 100px;
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
}

.scroll-wrapper {
    overflow-x: auto;
}

.days-row,
.bookings-row {
    display: flex;
    flex-wrap: nowrap;
    min-width: 800px;
}

.day-box {
    width: 500px;
    height: 40px;
    text-align: center;
    font-weight: bold;
    color: #000;
    margin-right: 100px;
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

.booking-day-slot {
    position: relative;
    min-height: 250px;
}

.booking-block {
    margin: 5px;
    padding: 5px 10px;
    color: #fff;
    font-size: 13px;
    border-radius: 4px;
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
}

.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 999;
}

.modal {
    background: #fff;
    padding: 20px;
    border-radius: 8px;
    width: 400px;
    max-width: 95%;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
}

.modal h3 {
    margin-top: 0;
}

.modal label {
    display: block;
    margin-top: 10px;
}

.modal input {
    width: 100%;
    margin-bottom: 10px;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 4px;
}

.button-group {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
}

.save-button {
    background-color: #f39c12;
    color: white;
    border: none;
    padding: 10px 15px;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.save-button:hover {
    background-color: #e08f08;
}

.timeline::-webkit-scrollbar {
    height: 8px;
}

.timeline::-webkit-scrollbar-thumb {
    background: #ccc;
    border-radius: 4px;
}
</style>