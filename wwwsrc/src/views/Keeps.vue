<template>
  <div>
    <div class="container-fluid">
      <div class="row">
        <div class="col-12">
          <h1>Keeps</h1>
        </div>
      </div>
      <div class="col-4">
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addkeep">
          add keep
        </button>
        <!-- ADD KEEP MODAL -->
        <div class="modal fade" id="addkeep" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
          aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">make a new keep</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form @submit.prevent="makeKeep()">
                  <div><input type="text" placeholder="Name" v-model="newKeep.name"></div>
                  <div><input type="text" placeholder="Description" v-model="newKeep.description"></div>
                  <div><input type="text" placeholder="Image Url" v-model="newKeep.img"></div>
                  <div><input type="checkbox" v-model="newKeep.isPrivate">Private</div>
                  <button type="submit" class="btn btn-primary">Add Keep</button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- KEEPS -->
      <div class="row">
        <div class="col-12 d-flex flex-wrap">
          <div class="card" style="width: 18rem;" v-for="keep in userKeeps">
            <img :src="keep.img" class="card-img-top" alt="picture">
            <div class="card-body">
              <h5 class="card-title">{{keep.name}}</h5>

              <!-- Button trigger modal -->

              <button type="button" class="btn btn-primary" @click="activeKeep.id = keep.id;update(keep.id)"
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
                      {{keep.description}}
                      <h5>views: {{keep.views}} keeps:{{keep.keeps}} shares: {{keep.shares}}</h5>
                    </div>
                    <div class="modal-footer">
                      <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                          data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                          add to vault
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                          <a class="dropdown-item" v-for="vault in vaults"
                            @click="addKeepToVault(vault.id, activeKeep.id)">{{vault.name}}</a>
                        </div>
                      </div>
                      <button type="button" @click="deleteKeep(keep.id)" class="btn btn-danger">delete</button>

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
    name: 'keeps',
    data() {
      return {
        newKeep: {
          name: "",
          description: "",
          img: "",
          isPrivate: 0
        },
        activeKeep: {
          id: 0,


        }
      }
    },
    mounted() {

      this.$store.dispatch('getUserKeeps');
      this.$store.dispatch('getVaults')
    },
    computed: {
      userKeeps() {
        return this.$store.state.userKeeps
      },
      vaults() {
        return this.$store.state.vaults
      },
      user() {
        return this.$store.state.user
      },
      activeVault() {
        return this.$store.state.activeVault
      }
    },
    methods: {
      makeKeep() {
        this.$store.dispatch('newKeep', this.newKeep);
        this.$store.dispatch('getUserKeeps');
      },
      deleteKeep(id) {
        this.$store.dispatch('deleteKeep', id)
      },
      update(Id) {

        let keep = this.$store.state.publicKeeps.find(keep => keep.id == Id)
        if (keep) {
          keep.views++
          this.$store.dispatch("updateKeep", keep)

        } else if (!keep) {
          let privateKeep = this.$store.state.userKeeps.find(keep => keep.id == Id)
          privateKeep.views++
          this.$store.dispatch("updateKeep", privateKeep)

        }
      }
      ,
      addKeepToVault(vId, kId) {

        this.$store.dispatch("activeVault", vId)
        // debugger
        let k = this.activeVault.keeps.find(keep => keep.id == kId)
        if (!k) {

          let keep = this.$store.state.publicKeeps.find(keep => keep.id == kId)
          if (keep) {
            keep.keeps++
            this.$store.dispatch("updateKeep", keep)

          } else if (!keep) {
            let privateKeep = this.$store.state.userKeeps.find(keep => keep.id == kId)
            privateKeep.keeps++
            this.$store.dispatch("updateKeep", privateKeep)
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
      }
    }
  }

</script>

<style>


</style>