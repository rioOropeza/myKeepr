<template>
  <div>

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
              <button type="button" class="btn btn-primary" @click="activeKeep = keep.id" data-toggle="modal"
                :data-target="'#'+keep.id">
                View Keep
              </button>
              <!-- VIEWING A SINGLE KEEP -->
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
                    <div class="modal-footer" v-if="user.email">
                      <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                          data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                          add to vault
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                          <a class="dropdown-item" v-for="vault in Vaults" @click="addKeepToVault(vault.id, activeKeep)">{{vault.name}}</a>
                        </div>
                      </div>
                      <button v-if="user.id == keep.userId" type="button" @click="deleteKeep(keep.id)" class="btn btn-danger">delete</button>

                    </div>
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
    name: "home",
    data() {
      return {
        activeKeep: 0
      }
    },
    mounted() {
      this.$store.dispatch('getPublicKeeps');
      this.$store.dispatch('getVaults');
    },
    computed: {
      publicKeeps() {
        return this.$store.state.publicKeeps
      },
      user() {
        return this.$store.state.user
      },
      Vaults() {
        return this.$store.state.vaults
      }
    },
    methods: {
      addKeepToVault(vId, kId) {
        let payload = {
          vaultId: vId,
          keepId: kId,
          user: this.user.id

        }
        this.$store.dispatch('addKeepToVault', payload)
      },
      deleteKeep(id) {
        this.$store.dispatch('deleteKeep', id)
      }
    }
  };
</script>