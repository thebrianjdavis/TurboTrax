import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if (currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    isLoggedIn: false,
    token: currentToken || '',
    user: currentUser || {},
    users: [],
    events: [],
    eventPlaylist: []
  },
  mutations: {
    SET_EVENT_PLAYLIST(state, playlist) {
      state.eventPlaylist = []; // clears any prior data??
      state.eventPlaylist = playlist;
    },
    GET_ALL_USERS(state, users) {
      state.users = users;
    },
    SET_EVENTS(state, events) {
      state.events = events;
    },
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user', JSON.stringify(user));
    },
    LOGOUT(state) {
      state.user.userId = 0;
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    FLIP_LOGIN_STATUS(state) {
      state.isLoggedIn = !state.isLoggedIn;
    }
  }
})
