<template>
  <router-view/>
</template>

<style>
@import "./components/css/common/index.css";
#app{
  height: 100% !important;
}
</style>
<script>
export default {
  watch:{
    $route : function(){
      if(this.$route.path == '/login' || this.$route.path == '/'){
        this.$cookies.keys().forEach((cookie) => this.$cookies.remove(cookie));
      }
    }
  }
}
</script>