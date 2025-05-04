import * as Yup from 'yup';

const autocompleteSchema = Yup.object().shape({
  title: Yup.string().min(2).max(50).required('Is Required'),
  summary: Yup.string().min(2).max(50).required('Is Required'),
  locationTypeId: Yup.string().min(1).required('Please select a LocationType').label('Selected LocationType'),
});

export default autocompleteSchema;