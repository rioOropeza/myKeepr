<template>

  <div class="vault container-fluid">
    <div class="row">
      <div class="col-12 d-flex flex-wrap">
        <div class="card" style="width: 18rem;" v-for="keep in activeVault.keeps">
          <img :src="keep.img" class="card-img-top" alt="picture">
          <div class="card-body">
            <h5 class="card-title">{{keep.name}}</h5>
            <h5>views: {{keep.views}} keeps:{{keep.keeps}} shares: {{keep.shares}}</h5>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'vault',
    data() {
      return {

      }
    },
    mounted() {
      this.$store.dispatch('activeVault', this.$route.params.vaultId)
    },
    computed: {
      activeVault() {
        return this.$store.state.activeVault
      }
    },
    activeVaultKeeps() {
      return this.$store.state.activeVaultKeeps
    },
    methods: {}
  }

</script>

<style>


</style>