<template>
  <div>
    <navbar />
    <div class="home container-fluid">
      <div class="row">
        <div class="col-12">
          <h1>Welcome Home</h1>
        </div>
      </div>
      <div class="row">
        <div class="col-12 d-flex flex-wrap">
          <div class="card" style="width: 18rem;" v-for="keep in publicKeeps">
            <img :src="keep.img" class="card-img-top" alt="picture">
            <div class="card-body">
              <h5 class="card-title">{{keep.name}}</h5>
              <a href="#" class="btn btn-primary">View Keep</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import navbar from "@/Components/Navbar.vue"
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch('getPublicKeeps');
    },
    computed: {
      publicKeeps() {
        return this.$store.state.publicKeeps
      }
    },
    components: {
      navbar
    }
  };
</script>