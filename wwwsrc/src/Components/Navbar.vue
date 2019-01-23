<template>
  <div class="pos-f-t padding">
    <div class="collapse" id="navbarToggleExternalContent">
      <div class="bg-dark p-4">
        <ul class="navbar-nav mt-2 mt-lg-0 menu">
          <router-link class="font" data-toggle="collapse" data-target="#navbarToggleExternalContent" to="/">Home</router-link>
          <router-link v-if="User.id" class="font" data-toggle="collapse" data-target="#navbarToggleExternalContent" to="/dashboard">Dashboard</router-link>
          <router-link v-if="User.id" @click.native='logout()' class="font" data-toggle="collapse" data-target="#navbarToggleExternalContent"
            to="/">Logout</router-link>
          <router-link v-if="!User.id" @click.native='login()' class="font" data-toggle="collapse" data-target="#navbarToggleExternalContent"
            to="/login">Login</router-link>
        </ul>
      </div>
    </div>
    <nav class="navbar navbar-dark bg-dark">
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent"
        aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
    </nav>
  </div>
</template>

<script>
  export default {
    name: 'navbar',
    data() {
      return {

      }
    },
    mounted() {

    },
    computed: {
      User() {
        return this.$store.state.user
      }
    },
    methods: {
      logout() {
        this.$store.dispatch('logout')
      }
    }
  }

</script>

<style>
  .padding {
    padding-bottom: 10px;
  }
</style>