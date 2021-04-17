<template>
  <div>
    <!-- 
    <div class="home-page">
      <h2 class="first-text">Welcome!</h2>
      <h2 class="second-text">Let's get this party started!</h2>
    </div>

    <div class="instructions">
      <h2 class="question">How to get your groove on?</h2>
      <h2 class="step-one">
        1. As a guest, click "See Events" to join a party!
      </h2>
      <h2 class="step-two">
        2. To set up an event, create an account and click "Create Event".
      </h2>
      <h2 class="step-three">
        3. Set your event details as the Dj and choose your host, then get your
        groove on!
      </h2>
    </div>
    -->
  </div>
</template>

<script>
export default {
  name: "home",
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Bebas+Neue&family=Fredoka+One&display=swap");

.home-page {
  background-image: url(http://blog.postpromdj.com/wp-content/uploads/2014/09/homepage2-e1410202267569.jpg);
  background-size: 100%;
  height: 100vh;
  display: flex;
}

.first-text,
.second-text,
.instructions {
  color: rgb(49, 118, 223);
  font-family: "Bebas Neue", cursive;
  font-family: "Fredoka One", cursive;
}

.first-text {
  display: flex;
  font-size: 400%;
  justify-content: flex-start;
  align-items: flex-start;
  margin-left: 37%;
  -webkit-text-stroke: 1px rgb(63, 0, 145);
}

.second-text {
  display: flex;
  align-items: flex-end;
  font-size: 400%;
  margin-bottom: 13%;
  margin-left: -40%;
  margin-right: auto;
  -webkit-text-stroke: 1px rgb(63, 0, 145);
}

.instructions {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: -150px;
}

.question {
  margin-bottom: 30px;
  font-size: 50px;
}

.step-one {
  margin-bottom: 25px;
}

.step-two {
  margin-bottom: 25px;
}

.step-three {
  margin-bottom: -10px;
}
</style>
