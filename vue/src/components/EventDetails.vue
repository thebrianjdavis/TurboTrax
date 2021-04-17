<template>
  <div id="main-div" class="events">
    <div
      class="event-div"
      v-for="event in $store.state.events"
      v-bind:key="event.eventId"
    >
      <h1>{{ event.eventName }}</h1>
      <h2>{{ event.eventDate }}</h2>
      <p>
        This event will go from {{ event.startTime }} to {{ event.endTime }}
      </p>
      <div class="button-spacing">
        <router-link
          :to="{
            name: 'eventDescription',
            params: { id: event.eventId },
          }"
          class="addMessage"
        >
          <v-btn class="btn" id="setup-button" color="white" rounded outlined medium>
            details
          </v-btn>
        </router-link>
        <router-link
          class="update-event"
          v-if="$store.state.user.userId == event.djUserId"
          :to="{ name: 'update', params: { id: event.eventId } }"
        >
          <v-btn class="btn" id="setup-button" color="white" rounded outlined medium>
            update
          </v-btn>
        </router-link>
        <router-link
          class="setup-event"
          v-if="$store.state.user.userId == event.hostUserId"
          :to="{ name: 'setup', params: { id: event.eventId } }"
        >
          <v-btn class="btn" id="setup-button" color="white" rounded outlined medium>
            setup
          </v-btn>
        </router-link>
      </div>
    </div>
  </div>
</template>

<!--
$store.state.user.userId
-->

<script>
import EventsService from "../services/EventsService.js"

export default {
  created() {
  EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
      this.event = this.$store.state.events.find((event) => {
        return event.eventId == this.$route.params.id;
      });
    });
}
};
</script>

<style>
#main-div {
  margin: auto;
  margin-bottom: -25px;
  width: 75%;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
  padding-top: 30px;
  color: rgb(252, 249, 249);
  font-family: "Titillium Web", sans-serif;
  margin-bottom: -50px;
}
.event-div {
  text-align: center;
  margin: auto;
  margin-top: 25px;
  padding: 10px;
  border: thin solid black;
  border-radius: 10px;
  width: 75%;
  background: rgba(0, 0, 0, 0.2);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
}
.btn {
  background: rgba(0, 0, 0, 0.3);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.6);
}
.button-spacing {
  display: flex;
  justify-content: space-around;
}
.container {
  padding: 0;
}

#setup-button {
  font-size: 1em;
  font-weight: bold;
}
.events {
  height: 100vh;
  overflow-y: scroll;
  margin-bottom: 25px;
  width: 100px;
  background: #ccc;
}
.events::-webkit-scrollbar-track {
  border: 1px solid #000;
  padding: 2px 0;
  background-color: #404040;
}
.events::-webkit-scrollbar {
  width: 10px;
}
.events::-webkit-scrollbar-thumb {
  border-radius: 10px;
  box-shadow: inset 0 0 6px rgba(0,0,0,.3);
  background-color: #737272;
  border: 1px solid #000;
}
</style>