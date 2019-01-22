<template>
  <div class="home">
    <h1>Welcome Home</h1>
    <div class="card" style="width: 18rem;" v-for="keep in publicKeeps">
      <img :src="keep.img" class="card-img-top" alt="picture">
      <div class="card-body">
        <p class="card-text">{{keep.name}}</p>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "home",
    mounted() {
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }

      this.$store.dispatch('getPublicKeeps');
    },
    computed: {
      publicKeeps() {
        return this.$store.state.publicKeeps
      }
    }
  };
</script>