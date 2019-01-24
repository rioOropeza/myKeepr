<template>
  <div>

    <div class="vaults container-fluid">
      <div class="row">
        <div class="col-12 d-flex flex-wrap">
          <div @click="" class="card text-white bg-dark mb-3" style="max-width: 18rem;" v-for="vault in Vaults">
            <router-link :to="{name: 'vault', params: {vaultId: vault.id}}">
              <div class="card-header">{{vault.name}}</div>
              <div class="card-body">
                <p class="card-text">{{vault.description}}</p>
              </div>
            </router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
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
    methods: {}
  }

</script>

<style>


</style>