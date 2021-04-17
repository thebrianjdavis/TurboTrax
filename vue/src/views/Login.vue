<template>
  <div id="login" class="text-center">
    <div class="signin-div">      
      <v-form id="signin-form" class="form-signin" @submit.prevent="login">
        <v-container id="form-container">
          <v-row>
            <v-col cols="12" sm="6">
              <div
                class="alert alert-danger"
                role="alert"
                v-if="invalidCredentials"
              >
                Invalid username and password!
              </div>
              <div
                class="alert alert-success"
                role="alert"
                v-if="this.$route.query.registration"
              >
                Thank you for registering, please sign in.
              </div>
              <v-text-field
                dark
                append-icon="mdi-login"
                id="username"
                label="Username"
                outlined
                v-model="user.username"
                required
                autofocus
              ></v-text-field>
            </v-col>
          </v-row>

          <v-row>
            <v-col class="password-div" cols="12" sm="6">
              <v-text-field
                dark
                id="password"
                label="Password"
                outlined
                v-model="user.password"
                :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show1 ? 'text' : 'password'"
                @click:append="show1 = !show1"
                required
                autofocus
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="login-button"  cols="6" sm="0">
              <v-btn
                x-large
                color="white"
                text
                class="btn btn-lg btn-primary btn-block"
                @click="login"
              >
                Login
              </v-btn>
            </v-col>
          </v-row>
        </v-container>
      </v-form>
    </div>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      show1: false,
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$store.commit("FLIP_LOGIN_STATUS");
            this.$router.push({name: 'welcome'});
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>

<style scoped>
.logo-div-show {
  position: fixed;
  top: 160px;
  right: 0;
}

#logo-img-show {
  width: 6em;
}

.signin-div {
  background: rgba(0, 0, 0, 0.4);;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
  padding: 200px 0;
  transform: skew(0deg, -10deg);
}

#signin-form {
  transform: skew(0deg, 10deg);
  padding-top: 7em;
  margin-bottom: -6em;
}

.password-div {
  height: 6em;
}

.login-button {
  text-align: left;
}
</style>