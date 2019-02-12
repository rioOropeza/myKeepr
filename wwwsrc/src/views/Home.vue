<template>
  <div>
    <div class="home container-fluid background">
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
              <button type="button" class="btn btn-primary" @click="activeKeep = keep.id;update(keep.id)"
                data-toggle="modal" :data-target="'#'+keep.id">
                View Keep
              </button>
              <div class="modal fade bd-example-modal-lg" :id="keep.id" tabindex="-1" role="dialog"
                aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                  <div class="modal-content">
                    <div class="modal-header">
                      <h5 class="modal-title" id="exampleModalLabel">{{keep.name}}</h5>
                      <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                    <div class="modal-body">
                      <img class="img-responsive" :src="keep.img" style="max-height:700px" alt="keep">
                      <h5>{{keep.description}}</h5>
                      <h5>views: {{keep.views}} keeps:{{keep.keeps}} shares: {{keep.shares}}</h5>
                    </div>
                    <div class="modal-footer" v-if="user.email">
                      <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                          data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                          add to vault
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                          <a class="dropdown-item" v-for="vault in Vaults"
                            @click="addKeepToVault(vault.id, activeKeep)">{{vault.name}}</a>
                        </div>
                      </div>
                      <button v-if="user.id == keep.userId" type="button" @click="deleteKeep(keep.id)"
                        class="btn btn-danger">delete</button>

                    </div>
                  </div>
                </div>
              </div>
              <!-- VIEWING A SINGLE KEEP -->
              <!-- <div class="modal fade" :id="keep.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
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
              </div> -->
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
      },
      activeVault() {
        return this.$store.state.activeVault
      }
    },
    methods: {
      addKeepToVault(vId, kId) {

        this.$store.dispatch("activeVault", vId)
        let k = this.$store.state.activeVault.keeps.find(keep => keep.id == kId)
        if (!k) {

          let keep = this.$store.state.publicKeeps.find(keep => keep.id == kId)
          if (keep) {
            keep.keeps++
            this.$store.dispatch("updateKeep", keep)

          }
          let payload = {
            vaultId: vId,
            keepId: kId,
            user: this.user.id
          }
          this.$store.dispatch('addKeepToVault', payload)
        } else {
          console.log('vault already contains that keep')
        }
        //dispatch to update the keeps on a keep
      },
      deleteKeep(id) {
        this.$store.dispatch('deleteKeep', id)
      },
      update(Id) {
        let keep = this.$store.state.publicKeeps.find(keep => keep.id == Id)
        if (keep) {
          keep.views++
          this.$store.dispatch("updateKeep", keep)
        }
      }
    }
  };
</script>
<style>
  /* .background {
    background-image: url("assets/splatter.jpg");
    height: 100%;
    background-attachment: fixed;
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
  } */
</style>