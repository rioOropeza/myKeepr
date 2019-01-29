<template>
  <div class="vault container-fluid">
    <div class="row">
      <div class="col-12">
        <h3>{{activeVault.name}}</h3>
      </div>
      <div class="col-12">

        <h3>{{activeVault.description}}</h3>
      </div>
      <button @click="deleteVault" type="button" class="btn btn-danger">delete vault</button>
      <div class="col-12 d-flex flex-wrap">
        <div class="card" style="width: 18rem;" v-for="keep in activeVault.keeps">
          <img :src="keep.img" class="card-img-top" alt="picture">
          <div class="card-body">
            <h5 class="card-title">{{keep.name}}</h5>
            <button type="button" class="btn btn-primary" @click="update(keep.id)" data-toggle="modal" :data-target="'#'+keep.id">
              View Keep
            </button>
            <div class="modal fade bd-example-modal-lg" :id="keep.id" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
              aria-hidden="true">
              <div class="modal-dialog modal-lg">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">{{keep.name}}</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                    <img class="img-responsive" style="max-height:700px" :src="keep.img" alt="keep">
                    {{keep.description}}
                    <h5>views: {{keep.views}} keeps:{{keep.keeps}} shares: {{keep.shares}}</h5>
                  </div>
                  <div class="modal-footer">
                    <button type="button" data-dismiss="modal" @click="removeKeep(activeVault.id, keep.id)" class="btn btn-danger">remove
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
      this.$store.dispatch('getUserKeeps');
    },
    computed: {
      activeVault() {
        return this.$store.state.activeVault
      },
      userKeeps() {
        return this.$store.state.userKeeps
      }
    },
    activeVaultKeeps() {
      return this.$store.state.activeVault.keeps
    },
    methods: {
      removeKeep(vaultId, keepId) {
        let keep = this.$store.state.publicKeeps.find(keep => keep.id == keepId)
        if (keep) {
          keep.keeps--
          this.$store.dispatch("updateKeep", keep)

        } else if (!keep) {
          let privateKeep = this.$store.state.userKeeps.find(keep => keep.id == keepId)
          privateKeep.keeps--
          this.$store.dispatch("updateKeep", privateKeep)

        }

        let payload = {
          vaultId: vaultId,
          keepId: keepId
        }
        this.$store.dispatch('removeKeepFromVault', payload)
      },
      update(Id) {
        let keep = this.$store.state.publicKeeps.find(keep => keep.id == Id)
        if (keep) {
          keep.views++
          this.$store.dispatch("updateKeep", keep)
          this.$store.dispatch('activeVault', this.$route.params.vaultId)
        } else if (!keep) {
          let privateKeep = this.$store.state.userKeeps.find(keep => keep.id == Id)
          privateKeep.views++
          this.$store.dispatch("updateKeep", privateKeep)
          this.$store.dispatch('activeVault', this.$route.params.vaultId)
        }
      },
      deleteVault() {
        this.$store.dispatch('deleteVault', this.$route.params.vaultId)
      }
    }
  }
</script>

<style>


</style>