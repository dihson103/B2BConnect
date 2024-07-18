import { z } from 'zod'

const configSchema = z.object({
  API_ENDPOINT: z.string(),
  BASE_URL: z.string(),
  LOGIN_PATH: z.string(),
  REGISTER_PATH: z.string(),
  ADMIN_PATH: z.string(),
  HOME_PATH: z.string(),
  DASHBOARD: z.string(),
  BUSINESS_PATH: z.string()
})

const configProject = configSchema.safeParse(process.env)

if (!configProject.success) {
  console.log(configProject.error.issues)
  throw new Error('Các giá trị khai báo trong env không hợp lệ.')
}

const envConfig = {
  ...configProject.data,
  LOGIN_URL: `${configProject.data.BASE_URL}${configProject.data.LOGIN_PATH}`,
  ADMIN_URL: `${configProject.data.BASE_URL}${configProject.data.ADMIN_PATH}`,
  HOME_URL: `${configProject.data.BASE_URL}${configProject.data.HOME_PATH}`,
  DASHBOARD_URL: `${configProject.data.BASE_URL}/admin/${configProject.data.DASHBOARD}`,
  REGISTER_URL: `${configProject.data.BASE_URL}${configProject.data.REGISTER_PATH}`,
  BUSINESS_URL: `${configProject.data.BASE_URL}${configProject.data.BUSINESS_PATH}`
}

export default envConfig
