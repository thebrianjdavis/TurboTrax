<template>
  <v-row justify="center">
  <v-dialog 
    max-width="600px"
    v-model="dialog"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          small
          class="mx-4"
          icon
          outlined
          fab
          dark
          color="blue darken-3"
          v-bind="attrs"
          v-on="on"
        >
          <v-icon dark> mdi-message-text </v-icon>
        </v-btn>
      </template>

    <v-card>
      <v-card-title>
        <h3>Add Your Shoutout!</h3>
      </v-card-title>
      <v-card-text>
        <v-form class="px-3">
          <v-textarea label="Enter Shoutout" v-model="shoutout"></v-textarea>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          class="mx-0 mt-3"
          color="blue darken-3"
          text
          @click="cancel"
        >
          Cancel
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn class="mx-0 mt-3" color="blue darken-3" text @click="submitNewShoutout">
          Submit
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  </v-row>
</template>


<script>
import SongsService from "../services/SongsService.js";

export default {
  props: ['songId', 'playlistId'],
  data() {
    return {
      dialog: false,
      shoutout: "",
      newSongShoutout: {
        PlaylistId: this.playlistId,
        SongId: this.songId,
        ShoutOutMessage: "",
      },

    };
  },
  methods: {
    cancel() {
      this.dialog = false;
      this.shoutout = "";
    },
    submitNewShoutout() {
      this.newSongShoutout.ShoutOutMessage = this.shoutout;
      if (this.shoutout != "") {
        SongsService.addSongShoutout(this.newSongShoutout).then((response) => {
          if (response.status == 201) {
            this.shoutout = "";
            this.dialog = false;
          }
        })
        .catch((error) => {
          alert(`Error: ${error.response.status} - ${error.response.statusText}`);
        });
      }

    }
  },
};
</script>