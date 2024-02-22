


Vue.component("file-uploader", {
    data() {
        return {
            maxFileSizeInMb: 2,
            skipConfirmation: false,
            fileList: [],
            i18nConfig: {
                upload: {
                    delete: 'Delete'
                }
            },
            supportedImageTypes: [
                { fileType: "image/apng", extension: ".apng" },
                { fileType: "image/avif", extension: ".avif" },
                { fileType: "image/gif", extension: ".gif" },
                { fileType: "image/jpeg", extension: ".jpeg" },
                { fileType: "image/jpeg", extension: ".jpg" },
                { fileType: "image/png", extension: ".png" }
            ]
        };
    },
    methods: {
        displayAllowedTypes() {
            return this.supportedImageTypes.map(itm => itm.extension).join(", ");
        },
        handleUploadAttempt(file, fileList) {
            const isAllowedType = this.supportedImageTypes.filter(itm => itm.fileType.toLowerCase() == file.type.toLowerCase()).length > 0;
            const fileSize = (file.size / 1024) / 1024;
            const isFileLimit = this.maxFileSizeInMb >= fileSize;

            if (!isAllowedType) {
                this.$message.error('Image is not a supported file type');
            }
            if (!isFileLimit) {
                this.$message.error(`Image is over the limit of ${maxFileSizeInMb}mb`);
            }
            this.skipConfirmation = true;
            return isAllowedType && isFileLimit;
        },
        handleSuccess(response, file) {
            this.fileList.push({ url: response.data.imageUrl, name: response.data.fileName });
        },
        handleExceed(files, fileList) {
            this.$message.warning(`You may only upload 1 image, this action would have resulted in ${files.length + fileList.length} uploads`);
        },
        handleError(err, file, fileList) {
            const errorResponse = JSON.parse(err.message);
            this.$message.error(`Failed to upload file: ${errorResponse.error.join(', ')}`);
        },
        handleDeleteAttempt(file, fileList) {
            if (this.skipConfirmation) {
                skipConfirmation = false
                return true;
            }
            var confirm = this.$confirm(`Are you sure that you want to remove this image?`, "Confirm deletion", {
                confirmButtonText: 'Confirm',
                cancelButtonText: 'Cancel',
                type: 'warning'
            });
            return confirm;
        }
    },
    template:
        `<div class="file-upload-container">
            <div>
                <div class="file-uploader-main"
                    <el-upload id="image-uploader"
                      action="/api/files/"
                      list-type="picture"
                      :limit="1"
                      :before-upload="handleUploadAttempt"
                      :before-remove="handleDeleteAttempt"
                      :on-exceed="handleExceed"
                      :on-success="handleSuccess"
                      :on-error="handleError"
                      :file-list="fileList">
                        <el-button size="small" type="secondary">
                            Select Image
                        </el-button>
                        <div slot="tip" class="el-upload__tip">
                            Only image files ( {{ displayAllowedTypes() }} ) with a size less than {{ maxFileSizeInMb }}mb are allowed.
                        </div>
                    </el-upload>
                </div>
            </div>
        </div>`,

});