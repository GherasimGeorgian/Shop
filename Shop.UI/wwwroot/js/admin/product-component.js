Vue.component('product-manager', {
    template: `<div>
                <div v-if="!editing">
                    <button class="button" @click="newProduct">Add new Product</button>
                    <table class="table">
                        <tr>
                            <td>Id</td>
                            <td>Product</td>
                            <td>Value</td>
                        </tr>
                        <tr v-for="(product, index) in products">
                            <td>{{product.id}}</td>
                            <td>{{product.name}}</td>
                            <td>{{product.description}}</td>
                            <td> {{product.value}}</td>
                            <td><a @click="editProduct(product, index)">Edit</a></td>
                            <td><a @click="deleteProduct(product.id, index)">Remove</a></td>
                        </tr>
                    </table>

                </div>


                <div v-else>

                    <div class="field">
                        <label class="label">Product Name</label>
                        <div class="control">
                            <input class="input" v-model="productModel.name">
                        </div>
                    </div>
                    <div class="field">
                        <label class="label">Product Description</label>
                        <div class="control">
                            <input class="input" v-model="productModel.description">
                        </div>
                    </div>
                    <div class="field">
                        <label class="label">Value</label>
                        <div class="control">
                            <input class="input" v-model="productModel.value">
                        </div>
                    </div>



                    <button class="button is-success" @click="createProduct" v-if="!productModel.id">Create Product</button>
                    <button class="button is-warning" @click="updateProduct" v-else>Update Product</button>
                    <button class="button" @click="cancel">Cancel</button>
                </div>
            </div>`,
    data() {
        return {
            editing: false,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product description",
                value: 1.99
            },
            loading: false,
            objectIndex: 0,
            products: []
        }
    },
    mounted() {
        //se executa doar o data
        this.getProducts();
    },
    methods: {

        getProduct(id) {
            this.loading = true;
            axios.get('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    // this.products = res.data;
                    this.productModel = {
                        id: res.data.id,
                        name: res.data.name,
                        description: res.data.description,
                        value: res.data.value,
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateProduct() {
            this.loading = true;
            axios.put('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
            this.productModel.id = 0;
            this.editing = true;
        },
        editProduct(product, index) {
            this.objectIndex = index;
            this.getProduct(product.id);
            this.editing = true;
        },
        cancel() {
            this.editing = false;
        },

    },
    computed: {

    }
});