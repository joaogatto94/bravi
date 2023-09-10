function validate(str) {
    const notClosedOpeners = [];
    const openers = ['(', '{', '['];
    const closers = [`)`, '}', ']'];

    for(let char of str) {
        if (openers.includes(char)) {
            notClosedOpeners.push(char);
        } else if (closers.includes(char)) {
            const charOpener = openers[closers.indexOf(char)];
            if (notClosedOpeners[notClosedOpeners.length - 1] === charOpener) {
                notClosedOpeners.pop();
            } else {
                break;
            }
        }
    }
    return !notClosedOpeners.length;
}