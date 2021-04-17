<template>
  <v-toolbar id="navigation" flat>
    <router-link 
      :to="{ name: 'home' }"
      v-if="$store.state.isLoggedIn === false"
      >
      <v-toolbar-title id="toolbar-title" class="text-uppercase white--text">
        <span class="font-weight-light">Turbo</span>
        <span>Trax</span>
      </v-toolbar-title>
    </router-link>

    <router-link 
      :to="{ name: 'welcome' }"
      v-if="$store.state.isLoggedIn === true"
      >
      <v-toolbar-title id="toolbar-title" class="text-uppercase white--text">
        <span class="font-weight-light">Turbo</span>
        <span>Trax</span>
      </v-toolbar-title>
    </router-link>

    <v-spacer></v-spacer>

    <router-link
      :to="{ name: 'home' }"
    >
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn class="nav mr-3" icon dark v-bind="attrs" v-on="on">
            <v-icon size="48">mdi-home-outline</v-icon>
          </v-btn>
        </template>
        <span>Home</span>
      </v-tooltip>
    </router-link>

    <router-link :to="{ name: 'events' }">
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn class="nav mr-3" icon dark v-bind="attrs" v-on="on">
            <v-icon size="40">mdi-calendar-star</v-icon>
          </v-btn>
        </template>
        <span>View Events</span>
      </v-tooltip>
    </router-link>

    <router-link
      :to="{ name: 'register' }"
      v-if="$store.state.isLoggedIn === false"
    >
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn class="nav mr-3" icon dark v-bind="attrs" v-on="on">
            <v-icon size="40">mdi-account-plus-outline</v-icon>
          </v-btn>
        </template>
        <span>Register</span>
      </v-tooltip>
    </router-link>

    <router-link
      :to="{ name: 'create' }"
      v-if="$store.state.isLoggedIn === true"
    >
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn class="nav mr-3" icon dark v-bind="attrs" v-on="on">
            <v-icon size="40">mdi-calendar-plus</v-icon>
          </v-btn>
        </template>
        <span>Create Event</span>
      </v-tooltip>
    </router-link>

    <router-link
      :to="{ name: 'login' }"
      v-if="$store.state.isLoggedIn === false"
    >
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn class="nav mr-3" icon dark v-bind="attrs" v-on="on">
            <v-icon size="40">mdi-login</v-icon>
          </v-btn>
        </template>
        <span>Login</span>
      </v-tooltip>
    </router-link>

    <router-link
      :to="{ name: 'logout' }"
      v-if="$store.state.isLoggedIn === true"
    >
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon dark v-bind="attrs" v-on="on">
            <v-icon size="40">mdi-logout</v-icon>
          </v-btn>
        </template>
        <span>Logout</span>
      </v-tooltip>
    </router-link>
  </v-toolbar>
</template>


<script>
export default {};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Syncopate&family=Train+One&display=swap");

#navigation {
  z-index: 10000;
  position: fixed;
  width: 100%;
  background: rgba(0, 0, 0, 0);
  color: white;
}
#toolbar-title {
  font-family: "Train One", cursive;
  font-size: 1.8em;
}
</style>