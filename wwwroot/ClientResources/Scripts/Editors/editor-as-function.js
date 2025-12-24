// example of editor that is not implemented in dojo, but a function
export default function customEditor(editorContainer, initialValue, onEditorValueChange) {
    const input = document.createElement("input");
    input.type = "text";
    input.value = initialValue || "";
    input.onchange = (event) => onEditorValueChange(event.target.value);
    editorContainer.appendChild(input);
}
