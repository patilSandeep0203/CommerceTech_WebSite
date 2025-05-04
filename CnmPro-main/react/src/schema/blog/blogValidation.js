import * as Yup from "yup";

const blogValidation = Yup.object().shape({
    title: Yup
        .string()
        .max(50, "Maximum of 50 characters allowed")
        .required("Required"),
    subject: Yup
        .string()
        .max(50, "Maximum of 50 characters allowed")
        .required("Required"),
    imageUrl: Yup
        .string()
        .max(255, "Maximum of 255 characters allowed"),
    content: Yup
        .string()
        .max(4000, "Maximum of 4000 characters allowed")
        .required("Required"),
    datePublished: Yup
        .date()
 })
export default blogValidation;
