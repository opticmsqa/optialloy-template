// example of editor that is not implemented in dojo, but using importing modules mechanism to get code
export default function customEditor(editorContainer, initialValue, onEditorValueChange) {
    return {
        render: function () {
            const input = document.createElement("input");
            input.type = "text";
            input.value = initialValue || "";
            input.onchange = (event) => onEditorValueChange(event.target.value);
            editorContainer.appendChild(input);
            this._input = input;
        },
    };
}
