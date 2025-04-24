<template>
  <header class="header">
    <div class="search-container">
      <input type="text" placeholder="Search for rooms and offers" />
      <button class="add-room-button">Add room</button>
    </div>
    <div class="user-options">
      <i class="fas fa-bell icon"></i>
      <i class="fas fa-user-circle icon user-avatar"></i>
    </div>
  </header>
</template>
<script>
export default {
  name: 'Header'
};
</script>
<style scoped>
.header {
  padding: 20px;
  background-color: #ffffff;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  margin-left: 50px;
  margin-bottom: 50px;
  /* border-radius: 10px; */
}

.header-title h1 {
  color: #f39c12;
  margin: 0;
  font-size: 30px;
  font-weight: 600;
}

.search-container {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.search-container input {
  padding: 12px;
  border-radius: 30px;
  border: 1px solid #ddd;
  flex: 1;
  font-size: 16px;
  transition: border-color 0.3s ease;
}

.search-container input:focus {
  border-color: #f39c12;
}

.add-room-button {
  background-color: #f39c12;
  color: white;
  border: none;
  padding: 12px 20px;
  border-radius: 30px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.add-room-button:hover {
  background-color: #e67e22;
  transform: scale(1.05);
}

.user-options {
  display: flex;
  align-items: center;
}

.icon {
  font-size: 30px;
  margin-left: 20px;
  cursor: pointer;
  transition: transform 0.3s ease;
}

.icon:hover {
  transform: scale(1.1);
  color: #f39c12;
}
</style>