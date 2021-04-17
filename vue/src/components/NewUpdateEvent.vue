<template>
  <div class="wrapper">
    <v-form id="update-event-form" ref="form" lazy-validation v-on:submit.prevent="updateEvent">
      
      <!-- name -->
      <v-text-field
        v-model="updatedEvent.EventName"
        :counter="60"
        label="Update Name"
        append-icon="mdi-pencil-outline"
        dark
      ></v-text-field>

      <!-- description -->
      <v-textarea
        id="text-area"
        v-model="updateEvent.EventDescription"
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
            v-model="updateEvent.EventDate"
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
          v-model="updateEvent.EventDate"
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
            v-model="updateEvent.StartTime"
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
          v-model="updateEvent.StartTime"
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
            v-model="updateEvent.EndTime"
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
          v-model="updateEvent.EndTime"
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
        v-model="updateEvent.HostUserId"
        dark
        dense
      ></v-select>
      <v-btn type="submit" class="form-button" color="white" x-large text>
        Update
      </v-btn>

    </v-form>
  </div>
</template>

<script>
import UsersService from "../services/UsersService.js";
import EventsService from "../services/EventsService.js";

export default {
  name: "new-update-event",
  data() {
    return {
      dateMenu: false,
      startTimeMenu: false,
      endTimeMenu: false,
      updatedEvent: {
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
  created() {
    EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
    });
    UsersService.getUsers().then((users) => {
      this.$store.commit("GET_ALL_USERS", users);
    });
  },
  computed: {
    eventToEdit() {
      return this.$store.state.events.find((event) => {
        return event.eventId == this.$route.params.id;
      });
    },
    allItems() {
      return this.$store.state.users.data.map((user) => {
        return {
          value: user.userId,
          name: `${user.firstName} ${user.lastName}`,
        };
      });
    },
    formatDate() {
      const dateToFormat = this.eventToEdit.eventDate.split("/");

      let month = dateToFormat[0];
      let day = dateToFormat[1];
      let year = dateToFormat[2];

      dateToFormat.forEach((element) => {
        if (dateToFormat[0] === element && element.length === 1) {
          month = `0${element}`;
        } else if (dateToFormat[1] === element && element.length === 1) {
          day = `0${element}`;
        }
      });

      return `${year}-${month}-${day}`;
    },
  },
  methods: {
    allowedMinutes: (v) => v % 15 === 0,
    updateEvent() {
      this.updatedEvent.DjUserId = this.eventToEdit.djUserId;
      this.updatedEvent.PlaylistId = this.eventToEdit.playlistId;
      EventsService.updateEvent(this.$route.params.id, this.updatedEvent)
        .then((response) => {
          if (response.status === 201) {
            this.$router.push({ name: "events" });
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        });
    },
  },
};
</script>

<style>
#update-event-form {
  width: 26%;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
  margin: auto;
  padding: 4em 3em 3em 3em;
  text-align: center;
  overflow-y: auto;
  margin-bottom: -35px;
}
.v-application {
  margin-bottom: 0;
}
.form-button {
  color: white;
}
#text-area {
  resize: none;  
}
#update-event-form::-webkit-scrollbar {
  width: 3px;
  background-color: rgb(185, 177, 177); 
}
#update-event-form::-webkit-scrollbar-thumb {
  background-color: #444;
}
</style>