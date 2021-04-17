<template>
  <div class="form-div" justify="space-around" align="center">
    <form class="form-add-event" v-on:submit.prevent="createNewEvent">
      <h1 class="h3 mb-3 font-weight-normal">Create An Event</h1>
      <div>
        <label for="eventname">New Event Name: </label>
        <input
          type="text"
          id="eventname"
          class="form-control"
          placeholder="enter event name"
          v-model="event.EventName"
        />
      </div>
      <div>
        <label for="eventdescription">Event Description: </label>
        <textarea
          id="eventdescription"
          name="w3review"
          placeholder="Enter Event Description"
          rows="4"
          cols="50"
          v-model="event.EventDescription"
        ></textarea>
      </div>

      <div>
        <v-row justify="space-around" align="center">
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Date:</h2>
            <v-date-picker id="date" v-model="event.EventDate"></v-date-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Start:</h2>
            <v-time-picker
              id="start"
              v-model="event.StartTime"
              :max="end"
              ampm-in-title
              :allowed-minutes="allowedMinutes"
            ></v-time-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>End:</h2>
            <v-time-picker
              id="end"
              v-model="event.EndTime"
              :min="start"
              ampm-in-title
              :allowed-minutes="allowedMinutes"
            ></v-time-picker>
          </v-col>
        </v-row>
      </div>
      <div>
        <v-col
        class="d-flex"
        cols="12"
        sm="3"
      >
        <v-select
          :items="allItems"
          item-text="name"
          label="Select a host"
          dense
          solo
          v-model="event.HostUserId"
        ></v-select>
      </v-col>
      </div>
      <div>
        <button type="submit">Save Event</button>
      </div>
    </form>
  </div>
</template>

<script>
import EventsService from "../services/EventsService.js";

export default {
  name: "create-event",
  data() {
    return {
      event: {
        DjUserId: 0,
        HostUserId: "",
        PlaylistId: 2,
        EventName: "",
        EventDescription: "",
        EventDate: "",
        StartTime: "",
        EndTime: "",
      }
    };
  },
  computed: {
    allItems() {
      if(this.$store.state.users.data){
        return this.$store.state.users.data.map(
          user => {
            return {value: user.userId, name: `${user.firstName} ${user.lastName}`} 
          }
        )
      }
      else{
        return 0;
      }
    }
  },
  methods: {
    allowedMinutes: v => v % 15 === 0,
    createNewEvent() {
      this.event.HostUserId = 
      EventsService.addEvent(this.event)
        .then((response) => {
          console.log(response.status);
          if (response.status === 201) {
            this.$router.push({ name: "events" });
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - DANG! ${error.response.statusText}`
          );
        });
    },
  },
};
</script>

<style>
.form-div {
  border: solid black;
  padding: 25px;
}

div {
  margin-bottom: 15px;
}
</style>