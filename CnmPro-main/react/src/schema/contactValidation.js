import * as Yup from "yup";

const contactValidation = Yup.object().shape({
    name: Yup.string().min(2).max(200).required('Required'),
    surname: Yup.string().min(2).max(200).required('Required'),
    email: Yup.string().email('Invalid email format').required('Required'),
    subject: Yup.string().min(2).max(200).required('Required'),
    message: Yup.string().min(10).max(4000).required('Required'),
})

export default contactValidation