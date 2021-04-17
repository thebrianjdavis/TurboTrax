<template>
  <h1>Logout</h1>
</template>

<script>
export default {
  created() {
    this.$store.commit("LOGOUT");
    this.$store.commit("FLIP_LOGIN_STATUS");
    this.$router.push("/login");
  }
};
</script>
