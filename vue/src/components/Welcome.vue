<template>
  <div>
    <div class="logo-div">
      <img
        id="logo-img"
        src="https://i.pinimg.com/originals/02/62/f4/0262f4dd643e39d3a8a5376b08d51125.png"
        alt=""
      />
    </div>
    <div class="description-div">
      <h1 v-bind="user">Welcome {{ user.firstName }}!</h1>
      <h3 class="how-to">Let's get this party started</h3>
    </div>
    <div class="welcome-container instructions-div">
      <div class="grid-child create-playlist">
        <v-icon size="60" color="white">mdi-headphones</v-icon>
        <br />
        <h3>DJ</h3>
        <p>
          Create events, assign hosts, and have the final say over what music is
          played
        </p>
      </div>
      <div class="grid-child voting">
        <v-icon size="60" color="white">mdi-boombox</v-icon>
        <br />
        <h3>HOST</h3>
        <p>
          Decide an event's name, give it a description, and choose what
          genre's go into a playlist
        </p>
      </div>
      <div class="grid-child played-songs">
        <v-icon size="60" color="white">mdi-account-group</v-icon>
        <br />
        <h3>USER</h3>
        <p>
          Join events and have your voice heard by voting on which songs you
          want to hear
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import UsersService from "../services/UsersService.js";

export default {
  data() {
    return {
      usersArray: []
    }
  },
  created() {
    UsersService.getUsers().then((response) => {
      this.usersArray = response.data;
    });
  },
  computed: {
    user() {
      return this.usersArray.find((user) => {
        return user.userId == this.$store.state.user.userId;
      });
    },
  },
};
</script>

<style>
.welcome-container {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-gap: 80px;
  text-align: center;
  width: 75%;
  margin: auto;
  margin-top: 4em;
  font-family: "Titillium Web", sans-serif;
}
h3 {
  font-size: 2em;
}
.logo-div {
  margin-top: 15px;
}
</style>