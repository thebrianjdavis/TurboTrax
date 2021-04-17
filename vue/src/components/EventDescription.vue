<template>
  <div class="main-description">
    <div class="description-box">
      <popup />
      <h1>{{ event.eventName }}</h1>
      <h2>Hosted by: {{ event.hostName }}</h2>
      <h3>
        {{ event.eventDate }} from {{ event.startTime }} until
        {{ event.endTime }}
      </h3>
      <div class="event-description">
        <p>{{ event.eventDescription }}</p>
      </div>
    </div>
    <div>
      <iframe
        src="https://open.spotify.com/embed/playlist/6OnduA17QGmW6Ak91Uo4M6"
        width="800"
        height="360"
        frameborder="0"
        allowtransparency="true"
        allow="encrypted-media"
      ></iframe>
    </div>

    <div class="playlist-display">
      <div class="playlist-div-left" id="large-2">
        <v-card class="mx-auto" max-width="800" color="brown lighten-4" >
          <!-- Tool bar -->
          <v-toolbar 
            color="brown lighten-1" 
            dark
            flat
            >
            <v-toolbar-title>DJ's Current Playlist</v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn icon>
              <v-icon>mdi-music</v-icon>
            </v-btn>
          </v-toolbar>

          <table>
            <thead>
              <tr>
                <th>Embedded Player</th>
                <th>Add ShoutOut</th>
              </tr>
            </thead>

            <tr v-for="song in activePlaylist" v-bind:key="song.songId">
              <td>
                <iframe
                  v-bind:src="somethingElse + song.spotifyId"
                  width="300"
                  height="80"
                  frameborder="0"
                  allowtransparency="true"
                  allow="encrypted-media"
                ></iframe>
              </td>

              <td>
                <v-btn
                  v-if="$store.state.user.userId == event.djUserId"
                  class="mx-2"
                  fab
                  dark
                  small
                  color="primary"
                  v-on:click="removeFromThePlaylist(song)"
                >
                  <v-icon dark> mdi-minus </v-icon>
                </v-btn>
              </td>

              <td v-if="$store.state.user.userId != event.djUserId">
                <v-tooltip bottom>
                  <template v-slot:activator="{ on, attrs }">
                    <span v-bind="attrs" v-on="on"
                      ><enter-shoutout
                        v-bind:songId="song.songId"
                        v-bind:playlistId="event.eventId"
                    /></span>
                  </template>
                  <span>Enter Shoutout</span>
                </v-tooltip>
              </td>

              <td
                v-if="
                  hasShoutout(song.songId) &&
                  $store.state.user.userId == event.djUserId
                "
              >
                <v-tooltip bottom>
                  <template v-slot:activator="{ on, attrs }">
                    <span v-bind="attrs" v-on="on"
                      ><display-shoutout
                        v-bind:shoutouts="eventShoutOuts"
                        v-bind:songId="song.songId"
                    /></span>
                  </template>
                  <span>View Shoutout</span>
                </v-tooltip>
              </td>
            </tr>
          </table>
        </v-card>
      </div>

      <div class="playlist-div" id="large-1">
        <v-card class="mx-auto" max-width="700" color="brown lighten-4">
          <v-toolbar 
            color="brown lighten-1" 
            dark
            flat
            >
            <v-toolbar-title>Available Songs To Pick</v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn icon>
              <v-icon>mdi-music</v-icon>
            </v-btn>
          </v-toolbar>

          <!-- songs table -->
          <table>
            <thead>
              <tr>
                <th>Embedded Player</th>
                <th>Up Vote</th>
                <th>Down Vote</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="song in possibleSongs" v-bind:key="song.songId">
                <td>
                  <iframe
                    v-bind:src="somethingElse + song.spotifyId"
                    width="300"
                    height="80"
                    frameborder="0"
                    allowtransparency="true"
                    allow="encrypted-media"
                  ></iframe>
                </td>
                <td>
                  <v-btn
                    small
                    v-show="
                      !upVoted.includes(song.songId) &&
                      $store.state.user.userId != event.djUserId
                    "
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="green darken-3"
                    v-model="totalVotes"
                    v-on:click="upVote(song)"
                  >
                    <v-icon dark> mdi-thumb-up </v-icon>
                  </v-btn>

                  <v-btn
                    small
                    v-show="
                      upVoted.includes(song.songId) &&
                      $store.state.user.userId != event.djUserId
                    "
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="grey darken-2"
                  >
                    <v-icon dark> mdi-thumb-up </v-icon>
                  </v-btn>

                  <v-btn
                    small
                    v-if="$store.state.user.userId == event.djUserId"
                    class="mx-2"
                    fab
                    dark
                    color="indigo"
                    v-on:click="addToPlaylist(song)"
                  >
                    <v-icon dark> mdi-plus </v-icon>
                  </v-btn>
                </td>
                <td>
                  <v-btn
                    small
                    v-show="
                      !downVoted.includes(song.songId) &&
                      $store.state.user.userId != event.djUserId
                    "
                    id="dislikebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="red darken-2"
                    v-model="totalVotes"
                    v-on:click="downVote(song)"
                  >
                    <v-icon dark> mdi-thumb-down </v-icon>
                  </v-btn>
                  <v-btn
                    small
                    v-show="
                      downVoted.includes(song.songId) &&
                      $store.state.user.userId != event.djUserId
                    "
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="grey darken-2"
                  >
                    <v-icon dark> mdi-thumb-down </v-icon>
                  </v-btn>
                  <div v-if="$store.state.user.userId == event.djUserId">
                    <h3>{{ song.songScore }}</h3>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </v-card>
      </div>
    </div>
  </div>
</template>

<script>
import SongsService from "../services/SongsService.js";
import EventsService from "../services/EventsService.js";
import DisplayShoutout from "./DisplayShoutout.vue";
import EnterShoutout from "./EnterShoutout.vue";

export default {
  components: {
    DisplayShoutout,
    EnterShoutout,
  },
  data() {
    return {
      /* the possible playlist needs to be set when a user clicks on an event. 
      A get method needs to run that retrieves the playlist by its id. */
      dialog: false,
      activePlaylist: [],
      possibleSongs: [],
      songVote: {
        PlaylistId: 0,
        SongId: 0,
        VoteValue: 0,
      },
      addRemoveSong: {
        PlaylistId: 0,
        SongId: 0,
      },
      newSongShoutout: {
        PlaylistId: 0,
        SongId: 0,
        ShoutOutMessage: "",
      },
      totalVotes: 0,
      event: {},
      eventShoutOuts: [],
      upVoted: [],
      downVoted: [],
      upValue: 0,
      downValue: 0,
      temp: "2374M0fQpWi3dLnB54qaLX",
      somethingElse: "https://open.spotify.com/embed/track/",
    };
  },
  created() {
    EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
      this.event = this.$store.state.events.find((event) => {
        return event.eventId == this.$route.params.id;
      });
    });
    SongsService.getPossibleSongs(this.$route.params.id).then((response) => {
      this.possibleSongs = response.data;
    });
    SongsService.getPlaylistByEvent(this.$route.params.id).then((resp) => {
      this.activePlaylist = resp.data;
    });
    EventsService.getEventShoutouts(this.$route.params.id).then((response) => {
      this.eventShoutOuts = response.data;
      console.log(response.data);
    });
  },
  methods: {
    upVote(song) {
      song.hasUpvoted = true;
      song.hasDownvoted = false;
      this.upValue = 1;
      this.upVoted.push(song.songId);
      if (this.downVoted.includes(song.songId)) {
        this.downVoted = this.downVoted.filter((vote) => {
          vote != song.songId;
        });
        this.upValue = 2;
      }
      this.songVote.PlaylistId = this.event.eventId;
      this.songVote.SongId = song.songId;
      this.songVote.VoteValue = this.upValue;
      SongsService.vote(this.songVote)
        .then((response) => {
          if (response.status == 200) {
            console.log(response);
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        });
    },
    downVote(song) {
      song.hasDownvoted = true;
      song.hasUpvoted = false;
      this.downValue = -1;
      this.downVoted.push(song.songId);
      if (this.upVoted.includes(song.songId)) {
        this.upVoted = this.upVoted.filter((vote) => {
          vote != song.songId;
        });
        this.downValue = -2;
      }
      this.songVote.PlaylistId = this.event.eventId;
      this.songVote.SongId = song.songId;
      this.songVote.VoteValue = this.downValue;
      SongsService.vote(this.songVote)
        .then((response) => {
          if (response.status == 200) {
            console.log(response);
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        });
    },
    addToPlaylist(song) {
      this.addRemoveSong.PlaylistId = this.event.eventId;
      this.addRemoveSong.SongId = song.songId;
      this.activePlaylist.push(song);
      this.possibleSongs = this.possibleSongs.filter(
        (s) => s.songId != song.songId
      );
      SongsService.addSongToPlaylist(this.addRemoveSong)
        .then((response) => {
          if (response.status == 200) {
            console.log(response);
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        });
    },
    removeFromThePlaylist(song) {
      this.addRemoveSong.PlaylistId = this.event.eventId;
      this.addRemoveSong.SongId = song.songId;
      this.activePlaylist = this.activePlaylist.filter(
        (s) => s.songId != song.songId
      );
      SongsService.removeSongFromPlaylist(this.addRemoveSong)
        .then((response) => {
          if (response.status == 200) {
            console.log(response);
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        });
    },
    hasShoutout(songId) {
      console.log(songId);
      let found = false;
      this.eventShoutOuts.forEach((shoutout) => {
        if (songId == shoutout.songId && shoutout.shoutoutMessage != "") {
          found = true;
        }
      });
      return found;
    },
  },
};
</script>

<style>
.main-description {
  text-align: center;
  width: 60%;
  margin: auto;
  margin-bottom: -50px;
  background: rgba(0, 0, 0, 0.4);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
  padding-top: 30px;
}
.description-box {
  margin-top: 30px;
  color: rgb(241, 239, 239);
  font-family: "Titillium Web", sans-serif;

}
.event-description {
  text-align: center;
  width: 50%;
  margin: auto;
  margin-top: 5px;
  margin-bottom: 5px;
  padding: 10px;
}
.playlist-display {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-gap: 10px;
  max-height: 100%;
}
.media-player > img {
  width: 20%;
}
.playlist-div,
.playlist-div-left {
  width: auto;
  margin: auto;
  height: auto;
  max-height: 150%;
  overflow: auto;
}
a {
  text-decoration: none;
}
#large-2,
#large-1 {
  height: 100vh;
  overflow-y: scroll;
  margin-bottom: 25px;
  background: #ccc;
}

#large-2::-webkit-scrollbar-track {
  border: 1px solid #000;
  padding: 2px 0;
  background-color: #404040;
}
#large-2::-webkit-scrollbar {
  width: 10px;
}
#large-2::-webkit-scrollbar-thumb {
  border-radius: 10px;
  box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
  background-color: #737272;
  border: 1px solid #000;
}
#large-1::-webkit-scrollbar-track {
  border: 1px solid #000;
  padding: 2px 0;
  background-color: #404040;
}
#large-1::-webkit-scrollbar {
  width: 10px;
}
#large-1::-webkit-scrollbar-thumb {
  border-radius: 10px;
  box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
  background-color: #737272;
  border: 1px solid #000;
}
.description-box {
  margin-top: 35px;
  color: rgb(241, 239, 239);
}
</style>