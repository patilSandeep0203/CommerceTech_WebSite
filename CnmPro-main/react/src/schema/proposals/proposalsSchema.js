import * as Yup from "yup";

const proposalSchema = Yup.object().shape({
    bid: Yup.string().min(2, "Minimum of Two Characters Required").max(50).required("Bid is Required"),
    duration: Yup.string().min(3, "Minimum of Three Characters Required").max(50).required("Duration is Required"), 
    details: Yup.string().min(2, "Minimum of Two Characters Required").max(4000, "Maximum of 4000 Characters Allowed").required("Details are Required"), 
})

export default proposalSchema;