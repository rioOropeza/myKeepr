<template>

  <div class="vault container-fluid">
    <div class="row">
      <div class="col-12 d-flex flex-wrap">
        <div class="card" style="width: 18rem;" v-for="keep in activeVault.keeps">
          <img :src="keep.img" class="card-img-top" alt="picture">
          <div class="card-body">
            <h5 class="card-title">{{keep.name}}</h5>
            <h5>views: {{keep.views}} keeps:{{keep.keeps}} shares: {{keep.shares}}</h5>
            <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#'+keep.id">
              View Keep
            </button>
            <div class="modal fade" :id="keep.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
              aria-hidden="true">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">{{keep.name}}</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                    <img :src="keep.img" alt="keep">
                    {{keep.description}}
                    <h5>views: {{keep.views}} keeps:{{keep.keeps}} shares: {{keep.shares}}</h5>
                  </div>
                  <div class="modal-footer">
                    <button type="button" @click="removeKeep(activeVault.id, keep.id)" class="btn btn-danger">remove
                      from vault</button>
                  </div>
                </div>
              </div>
            </div>
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
    methods: {
      removeKeep(vaultId, keepId) {

        let payload = {
          vaultId: vaultId,
          keepId: keepId
        }
        this.$store.dispatch('removeKeepFromVault', payload)
      }
    }
  }
</script>

<style>


</style>