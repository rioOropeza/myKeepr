import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let production = !window.location.host.includes('localhost')
let baseUrl = production ? "https://my-keepr.herokuapp.com" : "//localhost:5000"

let auth = Axios.create({
  baseURL: baseUrl + "/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: baseUrl + "/api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    publicKeeps: [],
    userKeeps: [],
    vaults: [],
    activeVault: {},
    activeVaultKeeps: {},
    vault: {}
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps
    },
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    setActiveVault(state, vault) {
      state.activeVault = vault
    },
    setVault(state, Vault) {
      state.vault = Vault
    }

  },
  actions: {
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('Login Failed')
        })
    }, logout({ commit, dispatch }) {
      auth.delete('logout')
        .then(res => {
          commit('setUser', res.data)
        })
    },
    getPublicKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setPublicKeeps', res.data)
        })
    },
    getUserKeeps({ commit, dispatch }) {
      api.get('keeps/user')
        .then(res => {
          commit('setUserKeeps', res.data)
        })
    },
    getVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => {
          commit('setVaults', res.data)
        })
    },
    newKeep({ commit, dispatch }, keep) {
      api.post('keeps', keep)
        .then(res => {
          dispatch('getUserKeeps', res.data)
        })
    },
    deleteKeep({ commit, dispatch }, id) {
      api.delete('keeps/' + id)
        .then(res => {
          dispatch('getUserKeeps')
        })
    },
    updateKeep({ commit, dispatch }, payload) {

      api.put('keeps/' + payload.id, payload)
        .then(res => {
          dispatch('getUserKeeps')
        })
    },
    addKeepToVault({ commit, dispatch }, payload) {
      api.post('vaultkeeps', payload)
        .then(res => {
        })
    },
    activeVault({ commit, dispatch }, vaultId) {

      api.get('vaults/' + vaultId)
        .then(res => {
          let vault = res.data
          api.get('vaultkeeps/' + vaultId)
            .then(res => {
              vault.keeps = res.data
              commit("setActiveVault", vault)
            })
        })
    },
    removeKeepFromVault({ commit, dispatch }, payload) {
      api.put('vaultkeeps', payload)
        .then(res => {
          dispatch('activeVault', payload.vaultId)

        })

    },
    newVault({ commit, dispatch }, vault) {
      api.post('vaults', vault)
        .then(res => {
          dispatch('getVaults')
        })
    },
    deleteVault({ commit, dispatch }, vault) {
      api.delete('vaults/' + vault)
        .then(res => {
          dispatch('getVaults')
          router.push({ name: 'vaults' })
        })
    },
    getKeepsInVault({ commit, dispatch }, vaultId) {
      api.get('vaults/' + vaultId)
        .then(res => {
          let vault = res.data
          api.get('vaultkeeps/' + vaultId)
            .then(res => {
              vault.keeps = res.data
              commit("setVault", vault)
            })
        })
    }
  }
})
