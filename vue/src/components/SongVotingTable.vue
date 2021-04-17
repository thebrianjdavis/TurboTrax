<template>
  <div id="main-container">
    <table>
      <thead>
        <tr>
          <th>Party Playlist</th>
          <th>Song and Artis</th>
          <th>Vote Buttoms</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="song in possiblePlaylist" v-bind:key="song.songId">
          <td>
            <v-avatar rounded size="60">
              <img v-bind:src="song.albumCover" alt="">
            </v-avatar>
          </td>
          <td id="song-artist-info">{{song.songName}} - {{song.artistName}}</td>
          <td>
            <v-btn small v-if="upVoteBool === true" id="likebtn" class="mx-2" icon outlined fab dark color="green darken-3" v-model="totalVotes" v-on:click="toggleUpvote">
              <v-icon dark> mdi-thumb-up </v-icon>
            </v-btn>

            <v-btn small v-if="upVoteBool === false" id="likebtn" class="mx-2" icon outlined fab dark color="grey darken-2">
              <v-icon dark> mdi-thumb-up </v-icon>
            </v-btn>          

            <v-btn small v-if="downVoteBool === true" id="dislikebtn" class="mx-2" icon outlined fab dark color="red darken-2" v-model="totalVotes" v-on:click="toggleDownvote">
              <v-icon dark> mdi-thumb-down </v-icon>
            </v-btn>
          <v-btn small v-if="downVoteBool === false" id="likebtn" class="mx-2" icon outlined fab dark color="grey darken-2">
              <v-icon dark> mdi-thumb-down </v-icon>
          </v-btn>
          </td>            
        </tr>
      </tbody>
    </table>
  </div>  
</template>

<script>
export default {
  data() {
    return {
      possiblePlaylist: [
            {
            songId: 1,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'Friends in Low Places',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 2,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'The Thunder Rolls',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 3,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/5/5a/Garth_Brooks-Ropin%27_the_Wind_%28album_cover%29.jpg',
            songName: 'The River',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 4,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Boot Scootin Boogie',
            artistName: 'Brooks & Dunn',          
            },
            {
            songId: 5,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Neon Moon',
            artistName: 'Brooks & Dunn',          
            },
            {
            songId: 6,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Neon Moon',
            artistName: 'Brooks & Dunn',          
            },
            {
            songId: 7,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Neon Moon',
            artistName: 'Brooks & Dunn',          
            },
            {
            songId: 8,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Neon Moon',
            artistName: 'Brooks & Dunn',          
            },
            {
            songId: 9,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'Friends in Low Places',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 10,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'Friends in Low Places',
            artistName: 'Garth Brooks',          
            }
        ]
    }
  },
  methods: {
    toggleUpvote() {
          this.upVoteBool = !this.upVoteBool;
          this.downVoteBool = true;
      },
      toggleDownvote() {
          this.downVoteBool = !this.downVoteBool;
          this.upVoteBool = true;
      },
  }
};
</script>

<style>
thead {
  display: none;
}

#song-artist-info {
  padding-left: 20px;
  padding-right: 8px
}
</style>