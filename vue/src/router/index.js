import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import Events from '../views/Events.vue'
import EventDescription from '../views/EventDescription.vue'
import CreateEvent from '../views/CreateEvent.vue'
import SongVotingTable from '../components/SongVotingTable.vue'
import Update from '../views/Update.vue'
import Setup from '../views/Setup.vue'
import Create from '../components/Create.vue'
import Welcome from '../components/Welcome.vue'
import NewUpdateEvent from '../components/NewUpdateEvent'
import store from '../store/index'


Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/welcome",
      name: "welcome",
      component: Welcome,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/events",
      name: "events",
      component: Events,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/events/:id",
      name: "eventDescription",
      component: EventDescription,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/create-event",
      name: "createEvent",
      component: CreateEvent, 
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/update-event/:id",
      name: "updateEvent",
      component: Update, 
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/setup-event/:id",
      name: "setup",
      component: Setup,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/table",
      name: "table",
      component: SongVotingTable,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/create",
      name: "create",
      component: Create,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/update/:id",
      name: "update",
      component: NewUpdateEvent,
      meta: {
        requiresAuth: false
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
