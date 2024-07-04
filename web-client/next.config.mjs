/** @type {import('next').NextConfig} */
const nextConfig = {
    images: {
        // https://scontent.fhan17-1.fna.fbcdn.net
        remotePatterns: [{
            protocol: 'https',
            hostname: 'scontent.fhan17-1.fna.fbcdn.net',
            port: '',
            pathname: '/**'
        }]
    }
};

export default nextConfig;
