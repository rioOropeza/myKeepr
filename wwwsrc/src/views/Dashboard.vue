<template>
  <div>

    <h1>Welcome {{User.username}}</h1>
    <router-link :to="{name: 'keeps'}">
      <div class="jumbotron jumbotron-fluid">
        <div class="container">
          <h1 class="display-4">KEEPS</h1>
        </div>
      </div>
    </router-link>
    <router-link :to="{name: 'vaults'}">
      <div class="jumbotron jumbotron-fluid">
        <div class="container">
          <h1 class="display-4">VAULTS</h1>
        </div>
      </div>
    </router-link>
  </div>
</template>

<script>

  export default {
    name: 'dashboard',
    data() {
      return {

      }
    },
    computed: {
      User() {
        return this.$store.state.user
      }
    },
    methods: {}
  }

</script>

<style>


</style>