import { lazy } from 'react';
// -- Alphabetize --
const About = lazy(() => import('../pages/aboutus'));
const Blog = lazy(() => import('../pages/blog/Blogs'));
const BlogDetails = lazy(() => import('../pages/blog/BlogDetails'));
const ChangePassword = lazy(() => import('../pages/auth/ChangePassword'));
const Confirm = lazy(() => import('../pages/auth/Confirm'));
const ContactUs = lazy(() => import('../pages/contactus/ContactUs'));
const Cookies = lazy(() => import('../pages/cookies'));
const Comment = lazy(() => import('../components/comments/Comments'));
const Events = lazy(() => import('../pages/eventpublic/Events'));
const EventDetails = lazy(() => import('../components/events/EventDetails'));
const ExampleAutocomplete = lazy(() => import('../pages/location/ExampleAutocomplete'));
const Faq = lazy(() => import('../pages/faqs/Faq'));
const FileUploader = lazy(() => import('../pages/testfileuploader/Test'));
const ForgotPassword = lazy(() => import('../pages/forgotpassword/ForgotPassword'));
const Jobs = lazy(() => import('../pages/jobpublic/Jobs'));
const Landing = lazy(() => import('../pages/landing'));
const Login = lazy(() => import('../pages/auth/Login'));
const Messages = lazy(() => import('../pages/messages/Messages'));
const Organizations = lazy(() => import('../components/organizations/Organizations'));
const OrganizationFollowers = lazy(() => import('../pages/organizationfollowers/UserOrgs'));
const OrganizationsForm = lazy(() => import('../components/organizations/OrganizationsForm'));
const PageNotFound = lazy(() => import('../pages/error/PageNotFound'));
const PrivacyPolicy = lazy(() => import('../pages/privacypolicy'));
const Ratings = lazy(() => import('../pages/rating/Rating'));
const AverageRating = lazy(() => import('../pages/rating/AverageRating'));
const Register = lazy(() => import('../pages/auth/Register'));
const ResumeBuilder = lazy(() => import('../components/resumes/ResumeBuilder'));
const SendToken = lazy(() => import('../pages/auth/verify/SendToken.jsx'));
const ServerError = lazy(() => import('../pages/error/ServerError'));
const ShareStoryDetails = lazy(() => import('../pages/sharestory/StoryDetail'));
const ShareStoryList = lazy(() => import('../pages/sharestory/ListShareStory'));
const ShareStoryPage = lazy(() => import('../pages/sharestory/ShareStoryForm'));
const Surveys = lazy(() => import('../components/surveys/Survey'));
const Webinar = lazy(() => import('../pages/daily/Webinar'));
const CallOptions = lazy(() => import('../pages/daily/CallOptions'));
const VerifyToken = lazy(() => import('../pages/auth/verify/VerifyToken.jsx'));
const SurveyDesigner = lazy(() => import('../pages/surveys/designer/SurveyDesigner'));

const routes = [
    {
        path: '/designer',
        name: 'Designer',
        exact: true,
        element: SurveyDesigner,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/',
        name: 'Landing',
        exact: true,
        element: Landing,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/about',
        name: 'About Us',
        exact: true,
        element: About,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/contact',
        name: 'Contact',
        exact: true,
        element: ContactUs,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/cookies',
        name: 'Cookies',
        exact: true,
        element: Cookies,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/events',
        name: 'Events',
        exact: true,
        element: Events,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/event/:id/details',
        name: 'Event Details',
        exact: true,
        element: EventDetails,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/files',
        name: 'FileUploader',
        exact: true,
        element: FileUploader,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/register',
        name: 'Register',
        exact: true,
        element: Register,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/login',
        name: 'Login',
        exact: true,
        element: Login,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/jobs',
        name: 'Jobs',
        exact: true,
        element: Jobs,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/faq',
        name: 'Faq',
        exact: true,
        element: Faq,
        roles: [],
        isAnonymous: true,
    },

    {
        path: '/confirm',
        name: 'Verify Email',
        exact: true,
        element: Confirm,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/locationexample',
        name: 'ExampleAutocomplete',
        exact: true,
        element: ExampleAutocomplete,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/blog',
        name: 'Blog',
        exact: true,
        element: Blog,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/blogdetails/:blogId',
        name: 'BlogDetails',
        exact: true,
        element: BlogDetails,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/ratings/average',
        name: 'AverageRating',
        exact: true,
        element: AverageRating,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/ratings',
        name: 'Rating',
        exact: true,
        element: Ratings,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/sharestory',
        name: 'Share Story',
        element: ShareStoryPage,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
    {
        path: '/sharestories',
        name: ' Share Story',
        element: ShareStoryList,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
    {
        path: '/storydetails/:storyId',
        name: ' Story Details',
        element: ShareStoryDetails,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
    {
        path: '/forgotpassword',
        name: 'ForgotPassword',
        exact: true,
        element: ForgotPassword,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/changepassword',
        name: 'ChangePassword',
        exact: true,
        element: ChangePassword,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/messages',
        name: 'Messages',
        exact: true,
        element: Messages,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/followers',
        name: 'Followers',
        exact: true,
        element: OrganizationFollowers,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/comment',
        name: 'Comment',
        exact: true,
        element: Comment,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/resumebuilder',
        name: 'ResumeBuilder',
        element: ResumeBuilder,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
    {
        path: '/login/verify',
        name: 'Two Factor Authentication',
        exact: true,
        element: SendToken,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/login/verify/auth',
        name: 'Two Factor Authentication',
        exact: true,
        element: VerifyToken,
        roles: [],
        isAnonymous: true,
    },

    {
        path: '/webinar',
        name: 'Webinar',
        element: Webinar,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
    {
        path: '/webinarsetup',
        name: 'CallOptions',
        element: CallOptions,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
    {
        path: '/surveys/:surveyId',
        name: 'Surveys',
        element: Surveys,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
];

const privacyPolicy = [
    {
        path: '/privacypolicy',
        name: 'PrivacyPolicy',
        exact: true,
        element: PrivacyPolicy,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/cookies',
        name: 'Cookies',
        exact: true,
        element: Cookies,
        roles: [],
        isAnonymous: true,
    },
];

const organizations = [
    {
        path: '/organizations',
        name: 'Organization',
        exact: true,
        element: Organizations,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/organization/new',
        name: 'OrganizationForm',
        exact: true,
        element: OrganizationsForm,
        roles: [],
        isAnonymous: true,
    },
    {
        path: '/organization/:id/edit',
        name: 'OrganizationForm',
        exact: true,
        element: OrganizationsForm,
        roles: [],
        isAnonymous: true,
    },
];

const errorRoutes = [
    {
        path: '/error-500',
        name: 'Error - 500',
        element: ServerError,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
    {
        path: '*',
        name: 'Error - 404',
        element: PageNotFound,
        roles: [],
        exact: true,
        isAnonymous: true,
    },
];

var allRoutes = [...routes, ...errorRoutes, ...organizations, ...privacyPolicy];

export default allRoutes;
