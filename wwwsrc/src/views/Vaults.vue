<template>
  <div>
    <div class="vaults container-fluid">
      <div class="row">
        <div class="col4">
          <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addkeep">
            add vault
          </button>
          <!-- ADD KEEP MODAL -->
          <div class="modal fade" id="addkeep" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLabel">make a new vault</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="makeVault">
                    <div><input type="text" placeholder="Name" v-model="newVault.name"></div>
                    <div><input type="text" placeholder="Description" v-model="newVault.description"></div>
                    <button type="submit" class="btn btn-primary">Add vault</button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-12 d-flex flex-wrap">
          <div class="card text-white bg-dark mb-3" style="max-width: 18rem;" v-for="vault in Vaults">
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
        newVault: {
          name: "",
          description: ""

        }
      }
    },
    mounted() {
      this.$store.dispatch('getVaults');
    }
    ,
    computed: {
      Vaults() {
        return this.$store.state.vaults
      }
    },
    methods: {
      makeVault(vault) {
        this.$store.dispatch('newVault', this.newVault);
      }

    }
  }

</script>

<style>


</style>