<template>
  <div>
    <form class="form-add-event" v-on:submit.prevent="updateEvent">
      <h1 class="h3 mb-3 font-weight-normal">Update Event</h1>
      <div>
        <label for="eventname">New Event Name: </label>
        <input
          type="text"
          id="eventname"
          class="form-control"
          placeholder="enter event name"
          v-model="updatedEvent.EventName"
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
          v-model="updatedEvent.eventDescription"
        ></textarea>
      </div>

      <div>
        <v-row justify="space-around" align="center">
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Currently {{ formatDate }}</h2>
            <v-date-picker
              id="date"
              v-model="updatedEvent.EventDate"
            ></v-date-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Start: {{ eventToEdit.startTime }}</h2>
            <v-time-picker
              id="start"
              v-model="updatedEvent.StartTime"
              :max="end"
              ampm-in-title
              :allowed-minutes="allowedMinutes"
            ></v-time-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>End: {{ eventToEdit.endTime }}</h2>
            <v-time-picker
              id="end"
              v-model="updatedEvent.EndTime"
              :min="start"
              ampm-in-title
              :allowed-minutes="allowedMinutes"
            ></v-time-picker>
          </v-col>
        </v-row>
      </div>
      <div>
        <v-col class="d-flex" cols="12" sm="3">
          <v-select
            :items="allItems"
            item-text="name"
            label="Select a host"
            dense
            solo
            v-model="updatedEvent.HostUserId"
          ></v-select>
        </v-col>
      </div>
      <div>
        <v-btn type="submit" elevation="2" rounded small>Update Event</v-btn>
        <!-- <button type="submit">Update Event</button> -->
      </div>
    </form>
  </div>
</template>

<script>
import UsersService from "../services/UsersService.js";
import EventsService from "../services/EventsService.js";

export default {
  data() {
    return {
      updatedEvent: {
        DjUserId: 0,
        HostUserId: "",
        PlaylistId: 0,
        EventName: "",
        EventDescription: "",
        EventDate: "",
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
</style>