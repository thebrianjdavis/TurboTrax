<template>
  <div>
      <update-event />
  </div>
</template>

<script>
import UpdateEvent from '../components/UpdateEvent.vue'

export default {
    components: {
        UpdateEvent,
    },
    data() {
        return {
            event: {},
        }
    },
}
</script>

<style>

</style>