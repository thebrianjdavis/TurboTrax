<template>
  <div id="wrapper">
    <v-form
      id="setup-form"
      ref="form"
      lazy-validation
      v-on:submit.prevent="submitEventSetup"
    >
      <v-text-field
        v-model="event.EventName"
        :counter="60"
        label="Name"
        dark
      ></v-text-field>

      <v-textarea
        id="text-area"
        v-model="event.EventDescription"
        :counter="145"
        label="Description"
        height="50"
        dark
      >
      </v-textarea>

      <v-autocomplete
        :items="['Alternative', 'Country', 'Hip Hop', 'Pop', 'Rock']"
        label="Exclude Genres"
        v-model="genres"
        multiple
        dark
      ></v-autocomplete>
      <v-btn type="submit" class="form-button" color="white" x-large text>
        Save
      </v-btn>
    </v-form>
  </div>
</template>

<script>
import SongsService from "../services/SongsService.js";
import EventsService from "../services/EventsService.js";

export default {
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
        DjName: "",
        HostName: "",
        LoggedInUser: "",
      },
      genres: [],
    };
  },
  created() {
    EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
    });
  },
  computed: {
    oldEvent() {
      return this.$store.state.events.find((event) => {
        return event.eventId == this.$route.params.id;
      });
    },
    newEvent() {
      return this.setupEvent(this.event);
    },
  },
  methods: {
    submitEventSetup() {
      SongsService.genres(this.$route.params.id, this.genres)
        .then((response) => {
          if (response.status === 200) {
            EventsService.updateEvent(
              this.$route.params.id,
              this.newEvent
            ).then((response) => {
              if (response.status === 200 || response.status === 201) {
                SongsService.createPossibleSongs(this.$route.params.id)
                .then((resp) => {
                  if (resp.status === 200){
                    this.$router.push({ name: "events" });
                  }
                })
              }
            });
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        });
    },
    setupEvent(event) {
      if (event.EventName === "" && event.EventDescription === "") {
        return this.oldEvent;
      }
      else if (event.EventName === "" && event.EventDescription !== "") {
        return {
          DjUserId: this.oldEvent.djUserId,
          HostUserId: this.oldEvent.hostUserId,
          PlaylistId: this.oldEvent.playlistId,
          EventName: this.oldEvent.eventName,
          EventDescription: event.EventDescription,
          EventDate: this.oldEvent.eventDate,
          StartTime: this.oldEvent.startTime,
          EndTime: this.oldEvent.endTime,
          DjName: this.oldEvent.djName,
          HostName: this.oldEvent.hostName,
          LoggedInUser: this.oldEvent.loggedInUser,
        };
      } else if (event.EventDescription === "" && event.EventName !== "") 
      {
        return {
          DjUserId: this.oldEvent.djUserId,
          HostUserId: this.oldEvent.hostUserId,
          PlaylistId: this.oldEvent.playlistId,
          EventName: event.EventName,
          EventDescription: this.oldEvent.eventDescription,
          EventDate: this.oldEvent.eventDate,
          StartTime: this.oldEvent.startTime,
          EndTime: this.oldEvent.endTime,
          DjName: this.oldEvent.djName,
          HostName: this.oldEvent.hostName,
          LoggedInUser: this.oldEvent.loggedInUser,
        };
      } else {
        return {
          DjUserId: this.oldEvent.djUserId,
          HostUserId: this.oldEvent.hostUserId,
          PlaylistId: this.oldEvent.playlistId,
          EventName: event.EventName,
          EventDescription: event.EventDescription,
          EventDate: this.oldEvent.eventDate,
          StartTime: this.oldEvent.startTime,
          EndTime: this.oldEvent.endTime,
          DjName: this.oldEvent.djName,
          HostName: this.oldEvent.hostName,
          LoggedInUser: this.oldEvent.loggedInUser,
        };
      }
    }
  },
};
</script>

<style>
#setup-form {
  width: 25%;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
  margin: auto;
  margin-bottom: -50px;
  padding: 10em 3em 3em 3em;
  text-align: center;
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
#wrapper {
  margin-bottom: -4em;
}
</style>