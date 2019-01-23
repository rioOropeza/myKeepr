<template>
  <div>
    <navbar />
    <div class="vaults container-fluid">
      <div class="row">
        <div class="col-12 d-flex flex-wrap">
          <div @click="" class="card text-white bg-dark mb-3" style="max-width: 18rem;" v-for="vault in Vaults">
            <div class="card-header">{{vault.name}}</div>
            <div class="card-body">
              <p class="card-text">{{vault.description}}</p>
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
    name: 'vaults',
    data() {
      return {

      }
    },
    mounted() {
      this.$store.dispatch('getVaults');
    },
    computed: {
      Vaults() {
        return this.$store.state.vaults
      }
    },
    methods: {},
    components: { navbar }
  }

</script>

<style>


</style>