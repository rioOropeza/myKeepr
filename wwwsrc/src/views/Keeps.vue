<template>
  <div>
    <div class="container-fluid">
      <div class="row">
        <div class="col-8">
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
              <h5>views: {{keep.views}} keeps:{{keep.keeps}} shares: {{keep.shares}}</h5>
              <!-- Button trigger modal -->
              <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addtovault">
                add to vault
              </button>
              <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#'+keep.id">
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
                    </div>
                    <div class="modal-footer">

                      <button type="button" @click="deleteKeep(keep.id)" class="btn btn-danger">delete</button>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ADD TO VAULT Modal -->
              <div class="modal fade" id="addtovault" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                aria-hidden="true">
                <div class="modal-dialog" role="document">
                  <div class="modal-content">
                    <div class="modal-body">
                      <div v-for="vault in vaults"><button @click="addKeepToVault(vault.id, keep.id)">{{vault.name}}</button></div>
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
      }
    },
    methods: {
      makeKeep() {
        this.$store.dispatch('newKeep', this.newKeep);
      },
      deleteKeep(id) {
        this.$store.dispatch('deleteKeep', id)
      },
      updateKeep(id) {
        this.$store.dispatch('updateKeep', id)
      },
      addKeepToVault(vId, kId) {
        let payload = {
          vaultId: vId,
          keepId: kId,
          user: this.user.id

        }
        this.$store.dispatch('addKeepToVault', payload)
      }
    }
  }

</script>

<style>


</style>