<template>
 <v-row justify="center">
    <v-dialog
      v-model="dialog"
      width="600px"
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
            <v-icon dark> mdi-bullhorn </v-icon>        
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">Party Shout Out!</span>
        </v-card-title>
        <v-card-text>
          <li v-for="item in shoutoutItems" v-bind:key="item.id">{{item.shoutOutMessage}}</li>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="blue darken-3"
            text
            @click="dialog = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
export default {
    props: ['shoutouts', 'songId'],
    data () {
        return {
            dialog: false,
            shoutoutItems: []
        }
    },
    created () {
      this.shoutoutItems = this.shoutouts.filter(
        (s) => s.songId == this.songId
      );

    }
};
</script>