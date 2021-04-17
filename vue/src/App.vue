<template>
  <v-app id="inspire">
    <navbar />
    <v-main class="v-main">
      <div id="app">
        <router-view />
        <div v-if="$route.path === '/'">
          <div class="logo-div">
            <img
              id="logo-img"
              src="https://i.pinimg.com/originals/02/62/f4/0262f4dd643e39d3a8a5376b08d51125.png"
              alt=""
            />
          </div>
          <div class="description-div">
            <h1>Get your Party started with TurboTrax</h1>
            <h3 class="how-to">
              Let your friends control the music at your party.
            </h3>
            <h3>
              TurboTrax is a free app that let's your guests choose which music
              should be played.
            </h3>
          </div>
          <div class="grid-container instructions-div">
            <div class="grid-child create-playlist">
              <v-icon size="45" color="white"
                >mdi-numeric-1-circle-outline</v-icon
              >
              <br />
              <v-icon size="75" color="white"
                >mdi-playlist-music-outline</v-icon
              >
              <p>Create a custom playlist</p>
            </div>
            <div class="grid-child voting">
              <v-icon size="45" color="white"
                >mdi-numeric-2-circle-outline</v-icon
              >
              <br />
              <v-icon size="75" color="white">mdi-thumbs-up-down</v-icon>
              <p>Up-vote or down-vote songs</p>
            </div>
            <div class="grid-child played-songs">
              <v-icon size="45" color="white"
                >mdi-numeric-3-circle-outline</v-icon
              >
              <br />
              <v-icon size="75" color="white">mdi-music</v-icon>
              <p>Songs with the most votes are played</p>
            </div>
          </div>
        </div>
      </div>
    </v-main> 
  </v-app>
</template>

<script>
import Navbar from "./components/Navbar.vue"
import UsersService from "./services/UsersService.js"

export default {
  components: {
    Navbar
  },
  data: () => ({ drawer: false }),
  created() {
    UsersService.getUsers().then((users) => {
      this.$store.commit("GET_ALL_USERS", users);     
      console.log(users);
    });
  }
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Bebas+Neue&family=Fredoka+One&display=swap");
@import url("https://fonts.googleapis.com/css2?family=Titillium+Web:wght@300&display=swap");

.nav-button {
  margin-top: 40px;
}
.toolbar-title {
  font-family: "Bebas Neue", cursive;
  font-family: "Fredoka One", cursive;
  color: rgb(49, 118, 223);
  margin-top: 17px;
}
body {
  background-image: url(https://www.contiki.com/six-two/wp-content/uploads/2015/09/sarthak-navjivan-iTZOPe7BpTM-unsplash.jpg);
  background-size: cover;
  background-attachment: fixed;
  margin: 0;
  padding: 0;
}
.toolbar-title {
  font-family: "Titillium Web", sans-serif;
}
.grid-container {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-gap: 20px;
  text-align: center;
  width: 50%;
  margin: auto;
  margin-top: 4em;
  font-family: "Titillium Web", sans-serif;
}
#logo-img {
  width: 25%;
  margin-bottom: -1.5em;
}
.logo-div {
  text-align: center;
}
.how-to {
  margin-top: 0.2em;
}
.description-div {
  color: white;
  text-align: center;
  font-family: "Titillium Web", sans-serif;
}
.instructions-div div {
  color: white;
}
h1 {
  font-size: 4em;
}
h3 {
  font-size: 2em;
}
p {
  font-size: 1.5em;
}
.v-application{
  background: transparent !important; 
}
body {
  overflow-y: auto;
}
body::-webkit-scrollbar {
  width: 0px;
  background-color: #444; 
}
body::-webkit-scrollbar-thumb {
  background-color: #444;
}
</style>