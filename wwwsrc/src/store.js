import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let auth = Axios.create({
  baseURL: "http://localhost:5000/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: "http://localhost:5000/api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    publicKeeps: [],
    userKeeps: [],
    vaults: [],
    activeKeep: {}
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
    setActive(state, active) {
      state.activeKeep = active
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
      debugger
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
    setActive({ commit, dispatch }, id) {
      commit('setActive', id)
    }
  }
})