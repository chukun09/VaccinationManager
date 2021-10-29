<template>
  <p class="titleinput">
    {{ titleinput }}<span v-show="required">(<span id="star">*</span>)</span>
  </p>
  <div
    class="my-custom-select"
    :class="{ 'select-arrow-active': selecting, error: error }"
  >
    <div class="select-selected">
      <input
        :tabindex="tabindex"
        type="text"
        :id="id"
        v-model="text"
        @input="autoComplete()"
        @focus="autoComplete()"
        @keydown="changeOption($event)"
        @blur="onBlurInput()"
      />
      <div class="inputMessage" v-if="error">
        <p>{{ inputMessage }}</p>
        <div class="bottom"></div>
      </div>
    </div>
    <div class="select-items" v-if="selecting">
      <div
        v-for="(option, index) in options"
        @mousedown="select(option)"
        :key="index"
        :value="option.value"
        :class="{
          'same-as-selected': option.value === value,
          hide:
            !option.text.toLowerCase().startsWith(text.toLowerCase()) &&
            !selectIcon,
          hover: index === first && !selectIcon,
        }"
      >
        {{ option.text }}
      </div>
    </div>
    <span class="select-icon" @mousedown="toggleSelect()"
      ><i class="fas fa-chevron-down"></i
    ></span>
  </div>
</template>
<script>
export default {
  name: "BaseComboBox",
  data() {
    return {
      options: [],
      inputMessage: "Dữ liệu không tồn tại trong hệ thống",
      error: false,
      selecting: false,
      text: "",
      value: "",
      selectIcon: false,
      first: 0,
    };
  },
  props: {
    required: {
      default: false,
    },
    titleinput: {
      type: String,
      required: true,
    },
    data: {},
    url: {},
    nameValue: { required: true },
    nameText: { required: true },
    defaultValue: { default: "" },
    defaultText: { default: "" },
    type: {
      default: "select",
    },
    customSelect: {},
    itemName: {},
    id: {},
    tabindex: {},
  },
  watch: {
    customSelect: function () {
           this.error = false;
      this.setValue(this.customSelect);
      this.selecting = false;
    },
  },
  methods: {
    /**
     * set text to input element
     * author: nlanh
     */
    setText(text) {
      this.text = text;
    },
    /**
     * set value selected
     */
    setValue(value) {
      this.value = value;
      this.options.map((item) => {
        if (value === item.value) {
          this.setText(item.text);
        }
      });
    },
    /**
     * close dropdown
     */
    onClose() {
      this.selecting = false;
    },
    /**
     * toggle dropdown onlick icon and change icon class
     */
    toggleSelect() {
      this.selecting = !this.selecting;
      this.selectIcon = true;
    },
    /**
     * select option: on click set value and set text
     */
    select(option) {
      this.error = false;
      let selectedOption = this.options.find(
        (item) => item.value == option.value
      );
      this.setValue(selectedOption.value);
      this.selecting = false;
      this.$emit("select", { value: this.value, name: this.nameValue });
    },
    /**
     * onclick clear button set value/text to default or ""
     */
    clearInput() {
      if (this.defaultValue) {
        this.setValue(this.defaultValue);
        this.select({ value: this.defaultValue });
      } else {
        this.setValue(null);
        this.setText("");
        this.$emit("select", { value: this.value, name: this.nameValue });
      }
    },
    /**
     * on input show options match with input value and hover first match
     */
    autoComplete() {
      this.error = false;
      this.selectIcon = false;
      this.selecting = true;
      this.setValue("");
      this.first = -1;
      this.options.map((item, idx) => {
        if (item.text.toLowerCase().startsWith(this.text.toLowerCase())) {
          if (this.first == -1) this.first = idx;
          item.match = true;
        } else {
          item.match = false;
        }
      });
      if (this.first == -1) {
        this.error = true;
      }
    },
    /**
     * change option when by keyboard
     */
    changeOption(event) {
      switch (event.keyCode) {
        case 40: {
          let tmp = this.first;
          while (
            tmp < this.options.length - 1 &&
            !this.options[tmp + 1].match
          ) {
            tmp = tmp + 1;
          }
          tmp = tmp + 1;
          if (tmp < this.options.length) this.first = tmp;
          break;
        }
        case 38: {
          let tmp = this.first;
          while (tmp > 0 && !this.options[tmp - 1].match) {
            tmp = tmp - 1;
          }
          tmp = tmp - 1;
          if (tmp >= 0) this.first = tmp;
          break;
        }
        case 13: {
          this.select(this.options[this.first]);
          break;
        }
      }
    },
    /**
     * validate input when blur, if not match any option show error
     */
    onBlurInput() {
      if (!this.selectIcon) {
        let find = false;
        this.selecting = false;
        this.options.map((item) => {
          if (item.text.toLowerCase() == this.text.toLowerCase()) {
            find = true;
            this.select(item);
            return false;
          }
        });
        if (!find && this.text != "") {
          this.error = true;
        } else {
          this.error = false;
        }
      }
    },
    /**
     * function for parent call to reset combobox to default value or ""
     */
    resetInput() {
      if (this.defaultValue) {
        this.setValue(this.defaultValue);
      } else {
        this.setValue("");
        this.setText("");
        this.$emit("select", { value: this.value, name: this.nameValue });
      }
      this.selecting = false;
    },
  },
  created() {
    document.addEventListener("click", (e) => {
      let el = this.$refs["combobox"];
      if (el && !(el.contains(e.target) || el === e.target)) {
        this.onClose();
      }
    });
    this.options = [];
    if (this.defaultText) {
      this.options.push({
        value: this.defaultValue,
        text: this.defaultText,
      });
      this.value = this.defaultValue;
      this.text = this.defaultText;
    }

    if (this.itemName) {
      Array.prototype.push.apply(
        this.options,
        JSON.parse(localStorage.getItem(this.itemName))
      );
    }
  },
};
</script>
<style scoped>
@import "../../css/common/customSelect.css";
@import "../../css/common/inputMessage.css";
.titleinput {
  text-align: left;
  margin: 0px;
  display: flex;
  margin-top: 10px;
}
</style>