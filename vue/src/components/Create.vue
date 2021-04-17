<template>
  <div class="wrapper">
    <v-form
      id="create-event-form"
      ref="form"
      lazy-validation
      v-on:submit.prevent="createNewEvent"
    >
      <!-- name -->
      <v-text-field
        v-model="event.EventName"
        :counter="60"
        label="New Event Name"
        append-icon="mdi-pencil-outline"
        dark
      ></v-text-field>

      <!-- description -->
      <v-textarea
        id="text-area"
        v-model="event.EventDescription"
        :counter="145"
        label="Description"
        append-icon="mdi-message-text-outline"
        height="50"
        dark
      >
      </v-textarea>

      <!-- date -->
      <v-menu
        ref="menu"
        v-model="dateMenu"
        :close-on-content-click="true"
        transition="scale-transition"
        offset-y
        min-width="auto"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="event.EventDate"
            label="Date"
            append-icon="mdi-calendar"
            readonly
            v-bind="attrs"
            v-on="on"
            dark
            dense
          ></v-text-field>
        </template>
        <v-date-picker
          ref="picker"
          v-model="event.EventDate"
          :min="new Date().toISOString().substr(0, 10)"
          color="red darken-1"
          header-color="grey darken-4"
          width="350px"          
        ></v-date-picker>
      </v-menu>

      <!-- start time -->
      <v-menu
        ref="menu"
        v-model="startTimeMenu"
        :close-on-content-click="false"
        transition="scale-transition"
        offset-y
        min-width="auto"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="event.StartTime"
            label="Start Time"
            append-icon="mdi-clock-outline"
            readonly
            v-bind="attrs"
            v-on="on"
            dark
            dense
          ></v-text-field>
        </template>
        <v-time-picker
          v-model="event.StartTime"
          :max="end"
          ampm-in-title
          :allowed-minutes="allowedMinutes"
          color="red darken-1"
          header-color="grey darken-4"
          format="ampm"
        ></v-time-picker>
      </v-menu>

      <!-- end time -->
      <v-menu
        ref="menu"
        v-model="endTimeMenu"
        :close-on-content-click="false"
        transition="scale-transition"
        offset-y
        min-width="auto"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="event.EndTime"
            label="End Time"
            append-icon="mdi-clock-alert-outline"
            readonly
            v-bind="attrs"
            v-on="on"
            dark
            dense
          ></v-text-field>
        </template>
        <v-time-picker
          v-model="event.EndTime"
          :max="end"
          ampm-in-title
          :allowed-minutes="allowedMinutes"
          color="red darken-1"
          header-color="grey darken-4"
          format="ampm"
        ></v-time-picker>
      </v-menu>

      <!-- select host -->
      <v-select
        :items="allItems"
        item-text="name"
        label="Select a host"
        append-icon="mdi-account-plus-outline"
        v-model="event.HostUserId"
        dark
        dense
      ></v-select>
      <v-btn type="submit" class="form-button" color="white" x-large text>
        Save
      </v-btn>
    </v-form>
  </div>
</template>

<script>
import EventsService from "../services/EventsService.js";

export default {
  name: "create-event",
  data() {
    return {
      dateMenu: false,
      startTimeMenu: false,
      endTimeMenu: false,
      event: {
        DjUserId: 0,
        HostUserId: "",
        PlaylistId: 2,
        EventName: "",
        EventDescription: "",
        EventDate: null,
        StartTime: "",
        EndTime: "",
      },
    };
  },
  computed: {
    allItems() {
      return this.$store.state.users.data.map((user) => {
        return {
          value: user.userId,
          name: `${user.firstName} ${user.lastName}`,
        };
      });
    },
  },
  methods: {
    allowedMinutes: (v) => v % 15 === 0,
    createNewEvent() {
      this.event.HostUserId = EventsService.addEvent(this.event)
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
#create-event-form {
  width: 26%;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
  margin: auto;
  padding: 4em 3em 3em 3em;
  text-align: center;
  overflow-y: auto;
  margin-bottom: -50px;
}
.form-button {
  color: white;
}
#text-area {
  resize: none;  
}
#create-event-form::-webkit-scrollbar {
  width: 3px;
  background-color: rgb(185, 177, 177); 
}
#create-event-form::-webkit-scrollbar-thumb {
  background-color: #444;
}
</style>